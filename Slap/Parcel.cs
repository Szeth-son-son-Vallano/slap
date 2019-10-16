using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slap
{
    class Parcel
    {
        // attributes
        private char lane;
        
        private string AWB;
        private string selectCd;
        private string destLocCd;
        private string consigneePostal;
        private double kiloWeight;

        private double estimateVol;

        private bool clearedStatus;

        // constructors
        public Parcel()
        {
            lane = '0';

            AWB = "";
            selectCd = "";
            destLocCd = "";
            consigneePostal = "";
            kiloWeight = 0.0;

            estimateVol = 0.0;

            clearedStatus = false;
        }

        public Parcel(string AWB, string selectCd, string destLocCd, string consigneePostal, double kiloWeight)
        {
            lane = '0';

            this.AWB = AWB;
            this.selectCd = selectCd;
            this.destLocCd = destLocCd;
            this.consigneePostal = consigneePostal;
            this.kiloWeight = kiloWeight;

            estimateVol = 0.0;

            checkClearedStatus(selectCd);
        }

        // setter methods
        public void setLane(char lane)
        {
            this.lane = lane;
        }

        public void setEstimateVolume(double estimateVol)
        {
            this.estimateVol = estimateVol;
        }

        // getter methods
        public char getLane()
        {
            return lane;
        }

        public string getAWB()
        {
            return AWB;
        }

        public string getSelectCd()
        {
            return selectCd;
        }

        public string getDestLocCd()
        {
            return destLocCd;
        }

        public string getConsigneePostal()
        {
            return consigneePostal;
        }

        public double getKiloWeight()
        {
            return kiloWeight;
        }

        public double getEstimateVol()
        {
            return estimateVol;
        }

        public bool getClearedStatus()
        {
            return clearedStatus;
        }

        // other methods
        public double calculateEstimateVol(double avgDensity)
        {
            double estimateVolResult = kiloWeight * avgDensity;
            return estimateVolResult;
        }

        private void checkClearedStatus(string selectCd)
        {
            string[] clearedCodes = { "DIA", "DT", "PL", "DR" };

            string[] codes = selectCd.Split(',');

            foreach(string code in codes)
            {
                if (clearedCodes.Contains(code))
                {
                    clearedStatus = true;
                    break;
                }
            }
        }
    }
}