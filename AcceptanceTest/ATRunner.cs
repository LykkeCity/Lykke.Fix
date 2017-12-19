﻿using System;
using System.Collections.Generic;
using QuickFix;
using System.Threading;

namespace AcceptanceTest
{
    public class ATRunner
    {
        static bool _stopMe = false;

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("usage: AcceptanceTest CONFIG_FILENAME");
                System.Environment.Exit(2);
            }

            ILog debugLog = new ConsoleLog();//new FileLog("log", new SessionID("AT", "Application", "Debug")); 
            ThreadedSocketAcceptor acceptor = null;
            try
            {
                ATApplication testApp = new ATApplication(debugLog);
                testApp.StopMeEvent += new System.Action(delegate () { _stopMe = true; });

                SessionSettings settings = new SessionSettings(args[0]);
                IMessageStoreFactory storeFactory = new MemoryStoreFactory();
                ILogFactory logFactory = null;
                //if (settings.Get().Has("Verbose") && settings.Get().GetBool("Verbose"))
                //    logF = new FileLogFactory(settings);
                acceptor = new ThreadedSocketAcceptor(testApp, storeFactory, settings, logFactory);

                acceptor.Start();
                while (true)
                {
                    System.Threading.Thread.Sleep(1000);

                    // for tests of logout
                    if (_stopMe)
                    {
                        // this doesn't seem to work
                        // after stop, it doesn't seem to start up again
                        /*
                        acceptor.Stop();
                        Thread.Sleep(5 * 1000);
                        _stopMe = false;
                        acceptor.Start();
                         */
                    }
                }
            }
            catch (System.Exception e)
            {
                debugLog.OnEvent(e.ToString());
            }

            finally
            {
                if (acceptor != null)
                    acceptor.Stop();
            }

        }
    }
    class ConsoleLog : ILog
    {
        public void OnIncoming(string msg) { }

        public void OnOutgoing(string msg) { }

        public void OnEvent(string s)
        {
            Console.WriteLine(s);
        }

        public void Clear() { }
        public void Dispose() { }
    }
}