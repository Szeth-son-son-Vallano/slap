using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slap
{
    class Parcel
    {
        // given attributes
        private string _AWB;
        private string _consigneeCompany;
        private string _consigneeAddress;
        private string _consigneePostal;
        private string _selectCd;
        private string _destLocCd;
        private string _courierRoute;

        private int _pieceQty;

        private double _kiloWgt;
        
        // inferred attributes
        private double _estimateVolume;
        private bool _clearedStatus;
        private char _lane;

        // constructors
        public Parcel()
        {
            _AWB = "";
            _consigneeCompany = "";
            _consigneeAddress = "";
            _consigneePostal = "";
            _selectCd = "";
            _destLocCd = "";
            _courierRoute = "";

            _pieceQty = 0;

            _kiloWgt = 0.0;

            _estimateVolume = 0.0;
            _clearedStatus = false;
            _lane = '0';
        }

        public Parcel(
            string AWB,
            string consigneeCompany, string consigneeAddr, string consigneePostal,
            string selectCd, string destLocCd, string courierRoute, 
            int pieceQty, double kiloWgt)
        {
            this._AWB = AWB;
            this._consigneeCompany = consigneeCompany;
            this._consigneeAddress = consigneeAddr;
            this._consigneePostal = consigneePostal;
            this._selectCd = selectCd;
            this._destLocCd = destLocCd;
            this._courierRoute = courierRoute;
            this._pieceQty = pieceQty;
            this._kiloWgt = kiloWgt;

            _estimateVolume = 0.0;
            checkClearedStatus(selectCd);
            _lane = '0';
        }

        // getter and setter methods
        public string AWB
        {
            get { return _AWB; }
            set { _AWB = value; }
        }
        public string ConsigneeCompany
        {
            get { return _consigneeCompany; }
            set { _consigneeCompany = value; }
        }
        public string ConsigneeAddress
        {
            get { return _consigneeAddress; }
            set { _consigneeAddress = value; }
        }
        public string ConsigneePostal
        {
            get { return _consigneePostal; }
            set { _consigneePostal = value; }
        }
        public string SelectCd
        {
            get { return _selectCd; }
            set { _selectCd = value; }
        }
        public string DestLocCd
        {
            get { return _destLocCd; }
            set { _destLocCd = value; }
        }
        public string CourierRoute
        {
            get { return _courierRoute; }
            set { _courierRoute = value; }
        }
        public int PieceQty
        {
            get { return _pieceQty; }
            set { _pieceQty = value; }
        }
        public double KiloWgt
        {
            get { return _kiloWgt; }
            set { _kiloWgt = value; }
        }
        public double EstimatedVol
        {
            get { return _estimateVolume; }
            set { }
        }
        public bool ClearedStatus
        {
            get { return _clearedStatus; }
            set { }
        }
        public char Lane
        {
            get { return _lane; }
            set { _lane = value; }
        }

        // other methods
        public double calculateEstimateVol(double avgDensity)
        {
            double estimateVolResult = _kiloWgt * avgDensity;
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
                    _clearedStatus = true;
                    break;
                }
            }
        }
    }
}