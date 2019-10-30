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
        private static double _estimateVolumeMax;
        // estimated Average Density is derived from FedEx Dimensional Weight calculation
        // The Actual Weight is greater than the Dimensional Weight(L x W x H / 5000)
        public static double _estimateDensity_Cm3PerKg = 5000;
        public static double _estimateDensity_KgPerM3 = 200;

        // dynamic attributes
        private char _laneID;
        private double _estimateVolumeCur;
        private List<Parcel> _parcelList;
        private List<string> _courierRouteList;

        // constructors
        public RouteGroup(char laneID)
        {
            _laneID = laneID;
            _estimateVolumeCur = 0.0;
            _parcelList = null;
            _courierRouteList = null;
        }

        public static void setEstimateVolumeMax(double estimateVolumeMax)
        {
            _estimateVolumeMax = estimateVolumeMax;
        }

        // getter and setter methods
        public char laneID
        {
            get { return _laneID; }
            set { _laneID = value; }
        }
        public double estimateVolumeCur
        {
            get { return _estimateVolumeCur; }
            set { _estimateVolumeCur = value; }
        }
        public List<Parcel> parcelList
        {
            get { return _parcelList; }
            set { _parcelList = value; }
        }
        public List<string> courierRouteRange
        {
            get { return _courierRouteList; }
            set { _courierRouteList = value; }
        }
    }
}
