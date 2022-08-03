using Tizen.Flutter.Embedding;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using System.Reflection;
using System;

namespace Runner
{
    public class App : FlutterNUIApplication
    {
        Timer timer;
        protected override void OnCreate()
        {

            Window window = NUIApplication.GetDefaultWindow();
            // View root = new View()
            // {
            //     Size2D = new Size2D(1080, 1920),
            //     BackgroundColor = Color.Red,
            //     Layout = new LinearLayout()
            //     {
            //         LinearAlignment = LinearLayout.Alignment.Center,
            //         LinearOrientation = LinearLayout.Orientation.Vertical,
            //     }
            // };
            // window.Add(root);

            View parent1 = new View()
            {
                Size2D = new Size2D(1920, 1080),
                BackgroundColor = Color.Blue,
                Layout = new LinearLayout()
                {
                    LinearAlignment = LinearLayout.Alignment.Begin,
                    LinearOrientation = LinearLayout.Orientation.Horizontal,
                }
            };
            // window.Add(parent1);

            // Only show a text button.
            Button textButton = new Button();

            textButton.BackgroundImageBorder = new Rectangle(4, 4, 5, 5);
            textButton.Size = new Size(350, 350);
            textButton.TextLabel.Text = "NUI text button";

            parent1.Add(textButton);


            // View parent2 = new View()
            // {
            //     BackgroundColor = Color.Green,
            //     Size2D = new Size2D(420, 420),
            //     Position = new Position(150, 725),
            //     Layout = new LinearLayout()
            //     {
            //         LinearAlignment = LinearLayout.Alignment.Center,
            //         LinearOrientation = LinearLayout.Orientation.Vertical,
            //     }
            // };
            ImageView imageView = new ImageView("/opt/usr/globalapps/com.example.dali_sample2/shared/res/ic_launcher.png", true)
            {
                Size2D = new Size2D(500, 500),
            };
            parent1.Add(imageView);
            window.Add(parent1);

            NativeImageQueue queue = new NativeImageQueue(350, 350, NativeImageQueue.ColorFormat.RGBA8888);
            Type typ = typeof(NativeImageQueue).BaseType.BaseType.BaseType;
            FieldInfo type = typ.GetField("swigCPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            System.Runtime.InteropServices.HandleRef value = (System.Runtime.InteropServices.HandleRef)type?.GetValue(queue);
            ImageUrl url = queue.GenerateUrl();
            Tizen.Log.Debug("BBR", "url : " + url.ToString());
            // imageView.SetImage(url.ToString());
            // flutter setup
            WindowWidth = 350;
            WindowHeight = 350;
            RenderTarget = value.Handle;

            base.OnCreate();
            GeneratedPluginRegistrant.RegisterPlugins(this);

            // var a = new FluttreView(value.Handle);
            textButton.Clicked += (o, e) =>
            {
                Tizen.Log.Debug("BBR", "Clicked!!!!");
                imageView.SetImage(url.ToString());
                window.KeepRendering(1);
            };

            timer = new Timer(1000);
            timer.Tick += (obj, e) =>
            {
                Tizen.Log.Debug("BBR", "Tick!!!!");
                window.KeepRendering(1);
                return true;
            };
            timer.Start();

            // System.Timers.Timer timer = new System.Timers.Timer(1000);
            // timer.Elapsed += (obj, e) =>
            // {
            //     Tizen.Log.Debug("BBR", "Tick!!!!");
            //     window.KeepRendering(1);
            // };
            // timer.Start();

            imageView.TouchEvent += (obj, e) =>
            {
                Tizen.Log.Debug("BBR", "ImageView Touched[" + e.Touch.GetState(0).ToString() + "]");
                Tizen.Log.Debug("BBR", "X[" + e.Touch.GetLocalPosition(0).X + "] " + "Y[" + e.Touch.GetLocalPosition(0).Y + "]");
                Tizen.Log.Debug("BBR", "time[" + e.Touch.GetTime().ToString() + "]");
                Tizen.Log.Debug("BBR", "device id[" + e.Touch.GetDeviceId(0).ToString() + "]");
                Tizen.Log.Debug("BBR", "pointCount[" + e.Touch.GetPointCount().ToString() + "]");
                OnTouchTest(e);
                return false;

            };
            // try {
            //     NativeImageQueue queue = new NativeImageQueue(350, 350, NativeImageQueue.ColorFormat.RGBA8888);
            //     Type typ = typeof(NativeImageQueue).BaseType.BaseType.BaseType;
            //     FieldInfo type = typ.GetField("swigCPtr", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //     // IntPtr value = IntPtr.Zero;
            //     // value = (IntPtr)type?.GetValue(queue);
            //     global::System.Runtime.InteropServices.HandleRef value = (global::System.Runtime.InteropServices.HandleRef)type?.GetValue(queue);
            //     Tizen.Log.Debug("BBR", "value :" + value.Handle.ToString());
            //     Tizen.Log.Debug("BBR", "url :" + queue.GenerateUrl().ToString());
            // } catch (Exception e ) {
            //     Tizen.Log.Debug("BBR", e.ToString());
            // }

        }

        static void Main(string[] args)
        {
            var app = new App();
            app.Run(args);
        }
    }
}
