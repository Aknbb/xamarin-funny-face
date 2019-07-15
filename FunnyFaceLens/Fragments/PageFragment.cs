using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace Hw3_Akin
{
    public class PageFragment : Fragment
    {
        const string ARG_PAGE = "ARG_PAGE";
        private int mPage;
        public static PageFragment newInstance(int page)
        {
            var args = new Bundle();
            args.PutInt(ARG_PAGE, page);
            var fragment = new PageFragment
            {
                Arguments = args
            };
            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mPage = Arguments.GetInt(ARG_PAGE);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            switch (mPage)
            {
                case 2:
                    var flowersView = inflater.Inflate(Resource.Layout.flowers_layout, container, false);
                    ViewCreaters.flowersView.getFlowersView(flowersView, Resources);
                    return flowersView;
                case 3:
                    var smokeView = inflater.Inflate(Resource.Layout.cigarettes_layout, container, false);
                    ViewCreaters.smokeView.getSmokeView(smokeView, Resources);
                    return smokeView;
                case 4:
                    var goggles = inflater.Inflate(Resource.Layout.glass_layout, container, false);
                    ViewCreaters.goggleView.getGoggleView(goggles, Resources);
                    return goggles;
                default:
                    var processView = inflater.Inflate(Resource.Layout.process_layout, container, false);
                    ViewCreaters.processView.getProcessView(processView, Resources);
                    var mainScrollView = (ScrollView)processView.FindViewById(Resource.Id.processScrollView);
                    mainScrollView.FullScroll(FocusSearchDirection.Up);
                    return processView;
            }
        }
    }
}