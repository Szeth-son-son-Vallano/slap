using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slap
{
    class Lane
    {
        // static attributes
        private static double _estimateVolumeMax;

        // dynamic attributes
        private char _laneID;
        private double _estimateVolumeCur;
        private List<Parcel> _parcelList;

        // constructors
        public Lane(char laneID)
        {
            _laneID = laneID;
            _parcelList = null;
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
    }
}
