namespace MonopolyMoneyManager.BusinessController
{
    using MonopolyMoneyManager.BusinessObjects;
    using System;

    public class GamerController {
        public Gamer Gamer { get; set; }
        private bool isSender = false;
        public bool IsSender {
            get { return isSender;}
            set
            {
                isSender = value;
                OnSenderChanged?.Invoke();
            }
        }
        private bool isReceiver = false;
        public bool IsReceiver {
            get { return isReceiver;}
            set
            {
                isReceiver = value;
                OnReceiverChanged?.Invoke();
            }
        }
        public GamerController(Gamer gamer)
        {
            Gamer = gamer;
            IsSender = false;
            IsReceiver = false;
        }

        public void ResetSender(GamerController gc){
            if(gc != this)
                isSender = false;
        }
        public void Reset(){
            isReceiver = false;
            isSender = false;
            OnReceiverChanged?.Invoke();
            OnSenderChanged?.Invoke();
        }

        public event Action OnSenderChanged;

        public event Action OnReceiverChanged;
    }
}