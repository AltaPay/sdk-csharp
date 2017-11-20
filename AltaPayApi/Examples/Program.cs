using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    class Program
    {
        public static void Main(string[] args)
        {
            //ReleaseExamples releaseExamples = new ReleaseExamples();
            //releaseExamples.Release();

            //RefundExamples refundExamples = new RefundExamples();
            //refundExamples.SimpleRefund();

            CaptureExamples captureExamples = new CaptureExamples();
            captureExamples.SimpleCapture();
            captureExamples.SimplePartialCapture();
            captureExamples.ComplexCapture();
        }
    }
}
