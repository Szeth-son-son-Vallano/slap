using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slap
{
    class RouteGroup
    {
        // static attributes
        private static double _laneEstimateVolumeMax = 1.1 * 1.2 * 10;

        // dynamic attributes
        private int _routeGroupID;
        private double _laneEstimateVolumeCur;
        private List<Parcel> _parcelList;
        private List<string> _courierRouteList;
        private string _lanes;

        // constructors
        public RouteGroup(int routeGroupID, List<string> courierRouteList)
        {
            _routeGroupID = routeGroupID;
            _laneEstimateVolumeCur = 0.0;
            _parcelList = new List<Parcel>();
            _courierRouteList = courierRouteList;
            _lanes = "";
        }

        public static void setEstimateVolumeMax(double estimateVolumeMax)
        {
            _laneEstimateVolumeMax = estimateVolumeMax;
        }

        // getter and setter methods
        public double LaneEstimateVolumeMax
        {
            get { return _laneEstimateVolumeMax; }
            set { }
        }
        public int RouteGroupID
        {
            get { return _routeGroupID; }
            set { _routeGroupID = value; }
        }
        public double LaneEstimateVolumeCur
        {
            get { return _laneEstimateVolumeCur; }
            set { _laneEstimateVolumeCur = value; }
        }
        public List<Parcel> ParcelList
        {
            get { return _parcelList; }
            set { _parcelList = value; }
        }
        public List<string> CourierRoutes
        {
            get { return _courierRouteList; }
            set { _courierRouteList = value; }
        }
        public string Lanes
        {
            get { return _lanes; }
            set { _lanes = value; }
        }
    }
}
