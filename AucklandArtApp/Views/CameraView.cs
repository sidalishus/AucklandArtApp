using CoreGraphics;
using System.Linq;
using AVFoundation;
using UIKit;
using System.Collections.Generic;
using System;

namespace Views
{
    public class CameraView : UIView
    {

        public AVCaptureDevice camera;
        public AVCaptureDeviceInput cameraInput;
        public AVCaptureSession session;
        public AVCaptureVideoPreviewLayer layer;
        public List<AVCaptureDevice> availableDevices;
        private AVCaptureDeviceInput cameraDeviceInput;

        public CameraView()
            : base()
        {
        }

        public void ReleaseCamera()
        {
            if (session.Running)
                session.StopRunning();

            camera = null;
            session = null;
        }

        public void StartCamera(CGRect frame)
        {
            session = new AVCaptureSession{ SessionPreset = AVCaptureSession.PresetHigh };
            availableDevices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video).ToList();
            camera = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
            var cameras = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);

            if (!cameras.Any())
                return;

            foreach (var c in cameras)
            {
                camera = c;
            }
            cameraInput = AVCaptureDeviceInput.FromDevice(camera);

            if (cameraInput != null)
            {
                session.AddInput(cameraInput);
                session.StartRunning();

                layer = new AVCaptureVideoPreviewLayer(session);
                layer.Frame = frame;
                layer.VideoGravity = AVLayerVideoGravity.ResizeAspectFill;

                Layer.AddSublayer(layer);
            }

            var oldInputs = session.Inputs;

            foreach (var input in oldInputs)
            {
                session.RemoveInput(input);
            }
                
            camera = camera.Position == AVCaptureDevicePosition.Front ? availableDevices.FirstOrDefault(x => x.Position == AVCaptureDevicePosition.Back) : availableDevices.FirstOrDefault(x => x.Position == AVCaptureDevicePosition.Front);

            if (camera != null)
            {
                cameraDeviceInput = AVCaptureDeviceInput.FromDevice(camera);
                if (cameraDeviceInput != null)
                {
                    session.AddInput(cameraDeviceInput);
                }
            }
            session.CommitConfiguration();
        }

        public void CheckCamera()
        {
            if (!session.Running)
            {
                UIAlertView error = new UIAlertView("Auckland Art App", "Requires Camera Permissions", null, "OK", null);
                error.Show();
            }
        }

    }
}    