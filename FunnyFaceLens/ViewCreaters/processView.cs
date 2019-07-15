using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Vision;
using Android.Gms.Vision.Faces;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Hw3_Akin.ViewCreaters
{
     class processView
    {
        static private Canvas canvas;
        static private ImageView faceImage;
        static private Button buttonProcess;
        static private Button buttonClear;
        static private ImageButton buttonSave;
        static Bitmap flowers;
        static Bitmap goggles;
        static Bitmap smoke;
        static private List<Position> positions = new List<Position>();
        static Bitmap faceBitmap;
        static Bitmap tempBitmap;
        private static Boolean isCompressSuccessful = false;
        public static View getProcessView(View givenView, Android.Content.Res.Resources Resources)
        {
            faceImage = (ImageView)givenView.FindViewById(Resource.Id.ImageViewFace);
            if (faceBitmap == null)
            {
                faceBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.memo);
                faceBitmap = changeBitmapSize(faceBitmap, 1500);
                faceImage.SetImageBitmap(faceBitmap);
                tempBitmap = Bitmap.CreateBitmap(faceBitmap.Width, faceBitmap.Height, Bitmap.Config.Rgb565);
                canvas = new Canvas(tempBitmap);
                canvas.DrawBitmap(faceBitmap, 0, 0, null);
            }
            faceImage.SetImageDrawable(new BitmapDrawable(Resources, tempBitmap));
            if (flowers == null) flowers = BitmapFactory.DecodeResource(Resources, Resource.Drawable.flowers);
            if(goggles == null) goggles = BitmapFactory.DecodeResource(Resources, Resource.Drawable.glass3);
            if(smoke == null) smoke = BitmapFactory.DecodeResource(Resources, Resource.Drawable.cigarette);
            buttonProcess = (Button)givenView.FindViewById(Resource.Id.buttonProcess);
            buttonClear = (Button)givenView.FindViewById(Resource.Id.buttonClear);
            buttonSave = (ImageButton)givenView.FindViewById(Resource.Id.buttonSave);
            AlertDialog.Builder builder = new AlertDialog.Builder(givenView.Context)
            .SetTitle("Save the image")
            .SetMessage("Are you sure to save?")
            .SetPositiveButton("Yes", (senderAlert, args) => {
                saveImage();
                if (isCompressSuccessful)
                {
                    Toast.MakeText(givenView.Context, "Successfuly saved!", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(givenView.Context, "Failed!", ToastLength.Short).Show();
                }
            })
            .SetNegativeButton("No", (senderAlert, args) => {});
            buttonProcess.Click += delegate
            {
                positions.Clear();
                FaceDetector faceDetector = new FaceDetector.Builder(Android.App.Application.Context)
                .SetTrackingEnabled(true)
                .SetLandmarkType(LandmarkDetectionType.All)
                .SetMode(FaceDetectionMode.Fast)
                .Build();

                if (!faceDetector.IsOperational)
                {
                    Toast.MakeText(givenView.Context, "Error about faceDetector.", ToastLength.Long);
                    return;
                }   
                Frame frame = new Frame.Builder().SetBitmap(faceBitmap).Build();
                SparseArray sparseArray = faceDetector.Detect(frame);
                for (int i = 0; i < sparseArray.Size(); i++)
                {
                    Face face = (Face)sparseArray.ValueAt(i);
                    DetectLandMarks(face);
                };
                int leftEyeX = positions.Find(x => x.id == "leftEye").positionX;
                int rightEyeX = positions.Find(x => x.id == "rightEye").positionX;
                int rightEyeY = positions.Find(x => x.id == "rightEye").positionY;
                int eyeWidth = leftEyeX - rightEyeX;


                goggles = changeBitmapSize(goggles, eyeWidth * 2);
                int glassScaleHeight = goggles.GetScaledHeight(canvas);
                canvas.DrawBitmap(goggles, rightEyeX - (eyeWidth / 2) + 5, rightEyeY - (glassScaleHeight / 2) - 2, null);

                flowers = changeBitmapSize(flowers, eyeWidth * 2);
                int flowersScaleWidth = flowers.GetScaledWidth(canvas);
                int mouthY = positions.Find(x => x.id == "bottomMouth").positionY;
                int noseX = positions.Find(x => x.id == "nose").positionX;
                int eyeMouthDifference = mouthY - rightEyeY;
                canvas.DrawBitmap(flowers, noseX - (flowersScaleWidth / 2), rightEyeY - (glassScaleHeight / 2) - eyeMouthDifference, null);

                smoke = changeBitmapSize(smoke, eyeWidth * 2);
                int rightMouthX = positions.Find(x => x.id == "rightMouth").positionX;
                int rightMouthY = positions.Find(x => x.id == "rightMouth").positionY;
                int cigaretteScaleWidth = smoke.GetScaledWidth(canvas);
                canvas.DrawBitmap(smoke, rightMouthX - (cigaretteScaleWidth - (cigaretteScaleWidth / 4)), rightEyeY - (glassScaleHeight / 2) + (eyeMouthDifference - (eyeMouthDifference / 8) - (eyeMouthDifference / 8)), null);

                faceImage.SetImageDrawable(new BitmapDrawable(Resources, tempBitmap));
            };


            buttonClear.Click += delegate
            {
                clearCanvas();
                
            };

            buttonSave.Click += delegate
            {
                var myCustomDialog = builder.Create();
                myCustomDialog.Show();
            };

            var gridView4 = givenView.FindViewById<GridView>(Resource.Id.gridView4);

            gridView4.Adapter = new Adapters.GridviewAdapters.peopleGridviewAdapter(givenView.Context);
            gridView4.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs args)
            {
                int id = (int)args.Id;
                faceBitmap = BitmapFactory.DecodeResource(Resources,id);
                faceBitmap = changeBitmapSize(faceBitmap, 1500);
                faceImage.SetImageBitmap(faceBitmap);
                tempBitmap = Bitmap.CreateBitmap(faceBitmap.Width, faceBitmap.Height, Bitmap.Config.Rgb565);
                canvas = new Canvas(tempBitmap);
                canvas.DrawBitmap(faceBitmap, 0, 0, null);
            };
            return givenView;
        }

        private static void DetectLandMarks(Face face)
        {
            foreach (var landmark in face.Landmarks)
            {
                collectTypeAndPositions(landmark.Type, (int)(landmark.Position.X), (int)(landmark.Position.Y));
            };
        }

        private static void collectTypeAndPositions(LandmarkType type, int positionX, int positionY)
        {
            switch (type)
            {
                case LandmarkType.NoseBase:
                    Position nose = new Position("nose", positionX, positionY);
                    positions.Add(nose);
                    return;

                case LandmarkType.LeftEye:
                    Position leftEye = new Position("leftEye", positionX, positionY);
                    positions.Add(leftEye);
                    return;

                case LandmarkType.RightEye:
                    Position rightEye = new Position("rightEye", positionX, positionY);
                    positions.Add(rightEye);
                    return;

                case LandmarkType.BottomMouth:
                    Position bottomMouth = new Position("bottomMouth", positionX, positionY);
                    positions.Add(bottomMouth);
                    return;

                case LandmarkType.RightMouth:
                    Position rightMouth = new Position("rightMouth", positionX, positionY);
                    positions.Add(rightMouth);
                    return;
            }

        }

        private static Bitmap changeBitmapSize(Bitmap givenBitmap, int givenWidth)
        {
            float aspectRatio = givenBitmap.Width / (float)givenBitmap.Height;
            int width = givenWidth;
            int height = (int)System.Math.Round(width / aspectRatio);
            Bitmap newSizedBitmap = Bitmap.CreateScaledBitmap(givenBitmap, width, height, false);
            return newSizedBitmap;
        }

        public static void setFlowers(Bitmap newBitmap)
        {
            processView.flowers = newBitmap;
        }

        public static void setGoogles(Bitmap newBitmap)
        {
            processView.goggles = newBitmap;
        }

        public static void setSmokes(Bitmap newBitmap)
        {
            processView.smoke = newBitmap;
        }

        private static void clearCanvas()
        {
            faceBitmap = changeBitmapSize(faceBitmap, 1500);
            faceImage.SetImageBitmap(faceBitmap);
            tempBitmap = Bitmap.CreateBitmap(faceBitmap.Width, faceBitmap.Height, Bitmap.Config.Rgb565);
            canvas = new Canvas(tempBitmap);
            canvas.DrawBitmap(faceBitmap, 0, 0, null);
        }
        private static void saveImage()
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var fileName = DateTime.Now.ToLongTimeString() + ".png";
            var filePath = System.IO.Path.Combine(sdCardPath, fileName);
            var stream = new FileStream(filePath, FileMode.Create);
            Boolean tempValue = faceImage.DrawingCacheEnabled;
            faceImage.DrawingCacheEnabled = true;
            faceImage.BuildDrawingCache();
            Bitmap bufferBitmap = Bitmap.CreateBitmap(faceImage.DrawingCache);
            isCompressSuccessful = bufferBitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            faceImage.DrawingCacheEnabled = tempValue;
            stream.Close();
        }
    }
}
