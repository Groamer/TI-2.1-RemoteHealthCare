﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fietsclient
{
    public class AppGlobal
    {
        private static AppGlobal _instance;

        Thread workerThread;
        public static AppGlobal Instance
        {
            get { return _instance ?? (_instance = new AppGlobal()); }
        }

        

        private KettlerBikeComm _bikeComm;


        private AppGlobal()
        {
            _bikeComm = new KettlerBikeComm();
            KettlerBikeComm.IncomingDataEvent += HandleBikeData; //initialize event
        }

        public void setTimeMode(string time)
        {
            _bikeComm.sendData("CU");
            _bikeComm.sendData("PT " + time);
        }

        public void setPower(string power)
        {
            _bikeComm.sendData("CU");
            _bikeComm.sendData("PW " + power);
        }

        public void setDistanceMode(string distance)
        {
            _bikeComm.sendData("CU");
            _bikeComm.sendData("PD " + distance);
        }

        public void startComPort()
        {
            startComPort("COM4");
        }

        public void startComPort(string portname)
        {
            _bikeComm.initComm(portname);
        }

        public void startAskingData()
        {
            workerThread = new Thread(() => workerThreadLoop());
            workerThread.Start();
        }

        private void workerThreadLoop()
        {
            while(true)
            {
                Thread.Sleep(1000);
                _bikeComm.sendData(KettlerBikeComm.STATUS);
            }
        }

        //event handler
        private void HandleBikeData(string[] data) 
        {
            //doe iets ermee...
        }

        public void closeComPort()
        {
            if (workerThread != null)
                workerThread.Suspend();
            _bikeComm.closeComm();
        }




    }
}
