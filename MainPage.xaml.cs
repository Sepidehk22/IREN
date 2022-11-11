using System;
using System.Threading.Tasks;

using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Azure.Communication;
using Azure.Communication.Calling;
using Azure.WinRT.Communication;

using System.Collections.Generic;
using System.Linq;
using static System.Net.WebRequestMethods;
using System.Net;


namespace CallingQuickstart
{
    public sealed partial class MainPage : Page
    {
      
        //string user_token_ = "<User_Access_Token>";
        string user_token_ = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjEwNiIsIng1dCI6Im9QMWFxQnlfR3hZU3pSaXhuQ25zdE5PU2p2cyIsInR5cCI6IkpXVCJ9.eyJza3lwZWlkIjoiYWNzOjhjZTQxZGU0LTkyMmUtNGY2MC05Mjg5LWNkNDQwMDg5NjI4NF8wMDAwMDAxNC1mY2RlLTQyMTQtN2JmYS01NTNhMGQwMDMwNjEiLCJzY3AiOjE3OTIsImNzaSI6IjE2NjgwMDczNzAiLCJleHAiOjE2NjgwOTM3NzAsImFjc1Njb3BlIjoiY2hhdCx2b2lwIiwicmVzb3VyY2VJZCI6IjhjZTQxZGU0LTkyMmUtNGY2MC05Mjg5LWNkNDQwMDg5NjI4NCIsInJlc291cmNlTG9jYXRpb24iOiJ1bml0ZWRzdGF0ZXMiLCJpYXQiOjE2NjgwMDczNzB9.vZmv9YnQvXjtI-HcCzc2s1V0Fn7z6A3IsNQmBE0DSfNK1kcVyBVhfa0xxsNc6eeHolC3-5Ao0O5J2FAq4ata-RATw--IVB2a0TgL8rAwcE2VpBjOfzoYLIGs2UpWdM7nP0y2tcWLxQIRqxKVvn4dJP3AzUggpgtMfrUH3Zfltjs0XoX4oq2Ym39bSlSSFm-KEJPHSdkvLowGVV8xfZ2hUJcKIHchtaf_HHmYn5TaDmokJYRYzK-et0G3Q-irofhbJxFvtVgxyOsFJzn1hxJ9gh4_XXmK5_A-BYzWG60W6Csk0QEJzkR0c2I1xcLzay5NdSeRXVJZi5VdNXDW7Pvj-Q";

        Call call_;
        CallClient call_client;
        CallAgent call_agent;
        DeviceManager deviceManager;
        Dictionary<string, RemoteParticipant> remoteParticipantDictionary = new Dictionary<string, RemoteParticipant>();
        VideoOptions video_;
        public MainPage()
        {
       
            this.InitializeComponent();
            call_client = new CallClient();

            Task.Run(() => this.InitCallAgentAndDeviceManagerAsync()).Wait();


            var callAgentOptions = new CallAgentOptions()

            {
                //User Id from Azure Communication Serivice
                DisplayName = "Ufficio Numero 1"
   
                            

            };

           

           

        }
        private async Task InitCallAgentAndDeviceManagerAsync()
        {
            // Initialize call agent and Device Manager
            var callClient = new CallClient();
           
            this.deviceManager = await callClient.GetDeviceManager();

        




            //video_.LocalVideoStreams  
            var tokenCredential = new CommunicationTokenCredential("eyJhbGciOiJSUzI1NiIsImtpZCI6IjEwNiIsIng1dCI6Im9QMWFxQnlfR3hZU3pSaXhuQ25zdE5PU2p2cyIsInR5cCI6IkpXVCJ9.eyJza3lwZWlkIjoiYWNzOjhjZTQxZGU0LTkyMmUtNGY2MC05Mjg5LWNkNDQwMDg5NjI4NF8wMDAwMDAxNC1mY2RlLTQyMTQtN2JmYS01NTNhMGQwMDMwNjEiLCJzY3AiOjE3OTIsImNzaSI6IjE2NjgwMDczNzAiLCJleHAiOjE2NjgwOTM3NzAsImFjc1Njb3BlIjoiY2hhdCx2b2lwIiwicmVzb3VyY2VJZCI6IjhjZTQxZGU0LTkyMmUtNGY2MC05Mjg5LWNkNDQwMDg5NjI4NCIsInJlc291cmNlTG9jYXRpb24iOiJ1bml0ZWRzdGF0ZXMiLCJpYXQiOjE2NjgwMDczNzB9.vZmv9YnQvXjtI-HcCzc2s1V0Fn7z6A3IsNQmBE0DSfNK1kcVyBVhfa0xxsNc6eeHolC3-5Ao0O5J2FAq4ata-RATw--IVB2a0TgL8rAwcE2VpBjOfzoYLIGs2UpWdM7nP0y2tcWLxQIRqxKVvn4dJP3AzUggpgtMfrUH3Zfltjs0XoX4oq2Ym39bSlSSFm-KEJPHSdkvLowGVV8xfZ2hUJcKIHchtaf_HHmYn5TaDmokJYRYzK-et0G3Q-irofhbJxFvtVgxyOsFJzn1hxJ9gh4_XXmK5_A-BYzWG60W6Csk0QEJzkR0c2I1xcLzay5NdSeRXVJZi5VdNXDW7Pvj-Q");
         


            var callAgentOptions = new CallAgentOptions()
            {
                DisplayName = "Iren ufficio numero 1"
                
            };
            var acceptCallOptions = new AcceptCallOptions();





            this.call_agent = await callClient.CreateCallAgent(tokenCredential, callAgentOptions);
         
        }
        private async void JoinButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (!await ValidateInput())
            {
                return;
            }
            var videoDeviceInfo = this.deviceManager.Cameras?.FirstOrDefault();
            if (videoDeviceInfo != null)
            {
          

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
               
                LocalVideo.Play();
               
                });
            }
          
            //  Create CallAgent
            //
            CommunicationTokenCredential token_credential;
            //try
            //{
                token_credential = new CommunicationTokenCredential(user_token_);
                CallAgentOptions call_agent_options = new CallAgentOptions();

            //}
            //catch
            //{
            //    await new MessageDialog("It was not possible to create call agent. Please check if token is valid.").ShowAsync();
            //    return;
            //}

            //
            //  Join a Teams meeting
            //
            //try
            //{
            JoinCallOptions joinCallOptions = new JoinCallOptions();

         
            var localVideoStream = new LocalVideoStream(videoDeviceInfo);
            if ((LocalVideo.Source == null) && (this.deviceManager.Cameras?.Count > 0))
            {
                if ((LocalVideo.Source == null) && (this.deviceManager.Cameras?.Count > 0))
                {

                    if (videoDeviceInfo != null)
                    {
                        // <Initialize local camera preview>
                        joinCallOptions.VideoOptions = new VideoOptions(new[] { localVideoStream });
                    }
                }

            }



            //TeamsMeetingLinkLocator teamsMeetingLinkLocator = new TeamsMeetingLinkLocator(TeamsLinkTextBox.Text);
            TeamsMeetingLinkLocator teamsMeetingLinkLocator = new TeamsMeetingLinkLocator("https://teams.microsoft.com/l/meetup-join/19%3ameeting_OTZmNmQ3ZWUtMDI2OC00MTVmLTk1ZGQtMDAwZmFhZTI4N2I0%40thread.v2/0?context=%7b%22Tid%22%3a%2272f988bf-86f1-41af-91ab-2d7cd011db47%22%2c%22Oid%22%3a%22b97195af-aeef-41eb-bd4c-8788aeb6a34f%22%7d");
            
            
            call_ = await call_agent.JoinAsync(teamsMeetingLinkLocator, joinCallOptions );

          
            if (this.deviceManager.Cameras?.Count > 0)
            {
                var startCallOptions = new StartCallOptions();
            
                if ((LocalVideo.Source == null) && (this.deviceManager.Cameras?.Count > 0))
                {
                    if ((LocalVideo.Source == null) && (this.deviceManager.Cameras?.Count > 0))
                    {
                       
                        if (videoDeviceInfo != null)
                        {
                            
                            joinCallOptions.VideoOptions = new VideoOptions(new[] { localVideoStream });
                        }
                    }

                }

            }


         
        }
        private async void Agent_OnIncomingCallAsync(object sender, IncomingCall incomingCall)
        {
           
        }
            private async void Call_OnStateChangedAsync(object sender, PropertyChangedEventArgs args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                CallStatusTextBlock.Text = call_.State.ToString();
            });
        }

    

        private async void LeaveButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            try
            {
                await call_.HangUpAsync(new HangUpOptions());
            }
            catch
            {
                await new MessageDialog("It was not possible to leave the Teams meeting.").ShowAsync();
            }
        }

        //Remote Partecipant
        private async void Call_OnVideoStreamsUpdatedAsync(object sender, RemoteVideoStreamsEventArgs args)
        {
            foreach (var remoteVideoStream in args.AddedRemoteVideoStreams)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    
                  
                    RemoteVideo.Source = await remoteVideoStream.Start();
                });
                //}

                //foreach (var remoteVideoStream in args.RemovedRemoteVideoStreams)
                //{
                remoteVideoStream.Stop();
                
            }
        }
        private async void Agent_OnCallsUpdatedAsync(object sender, CallsUpdatedEventArgs args)
        {
            foreach (var call in args.AddedCalls)
            {
                foreach (var remoteParticipant in call.RemoteParticipants)
                {
                    var remoteParticipantMRI = remoteParticipant.Identifier.ToString();
                    this.remoteParticipantDictionary.TryAdd(remoteParticipantMRI, remoteParticipant);
                    var remoteParticipantMute = remoteParticipant.IsMuted.ToString();
                  
                
                }
            }
        }
        private async Task AddVideoStreamsAsync(IReadOnlyList<RemoteVideoStream> remoteVideoStreams)
        {
            //Render remote videos
            foreach (var remoteVideoStream in remoteVideoStreams)
            {
                var remoteUri = await remoteVideoStream.Start();

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    RemoteVideo.Source = remoteUri;
                    RemoteVideo.Play();
                });
            }
        }
        private async Task<bool> ValidateInput()
        {
            if (user_token_.StartsWith("<"))
            {
                await new MessageDialog("Please enter token in source code.").ShowAsync();
                return false;
            }

            

            return true;
        }


    }
}