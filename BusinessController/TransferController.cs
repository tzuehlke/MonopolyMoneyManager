namespace MonopolyMoneyManager.BusinessController
{
    using System;
    using System.Collections.Generic;
    using MonopolyMoneyManager.BusinessObjects;

    public class TransferController {
        public bool TransferDisabled { get; set; } = false;
        public string Msg { get; set; } = "";
        public string Status { get; set; } = "bg-info";
        //public int Sum { get { return (receiver.Count == 0 ? TransferAmount : receiver.Count * TransferAmount) ;} }
        public int Sum {
            get {
                int amount = TransferAmountNullable??0;
                return (receiver.Count == 0 ? amount : receiver.Count * amount) ;}
            }
        private int? transferAmount = null; 
        /*public int TransferAmount {
            get{ return transferAmount;}
            set{
                transferAmount = value;
                UpdateGui();
            }
        }*/
        public int? TransferAmountNullable
        {
            get { return transferAmount;}
            set {
                transferAmount = value;
                UpdateGui();
            }
        }

        public event Action<GamerController> OnResetSender;

        public event Action OnResetAll;

        public TransferController()
        {
        }

        public void UpdateGui(){
            SetTransferEnabled();
            GenerateMsg();
        }

        List<Gamer> receiver = new List<Gamer>();
        List<Gamer> sender = new List<Gamer>();
        public void ChangeSender(GamerController g){
            //Console.WriteLine("ChangeSender...");
            /*if(g.IsSender){ 
                sender.Add(g.Gamer);
            }
            else sender.Remove(g.Gamer);*/
            sender.Clear();
            OnResetSender?.Invoke(g);
            if(g.IsSender) sender.Add(g.Gamer);
            UpdateGui();
            System.Console.Write("New Sender: ");
            foreach(var s in sender){
                System.Console.Write(s.Name);
            }
            System.Console.WriteLine();
        }
        public void ChangeReceiver(GamerController g){
            //Console.WriteLine("ChangeReceiver...");
            //System.Console.WriteLine("Changed: " + g.Gamer.Name + " is now " + g.IsReceiver);
            if(g.IsReceiver) receiver.Add(g.Gamer);
            else receiver.Remove(g.Gamer);
            
            UpdateGui();
            System.Console.Write("New Receiver: ");
            foreach(var r in receiver){
                System.Console.Write(r.Name);
            }
            System.Console.WriteLine();
        }

        private void GenerateMsg(){
            Console.WriteLine("GenerateMsg...");
            string msg_source = "";
            string msg_dest = "";
            foreach (var g in sender)
            {
                msg_source += g.Name + " ";
            }
            foreach (var g in receiver)
            {
                msg_dest += g.Name +" ";
            }
            if(receiver.Count == 0) msg_dest = "Bank";
            if(sender.Count == 0) msg_source = "Bank ";
            if(IsValid()) Status = "bg-info";
            else Status = "bg-warning";
            Msg = msg_source + "send " + Sum.ToString() + " 💰 to " + msg_dest;
        }

        private void SetTransferEnabled(){
            if(IsValid()) TransferDisabled = false;
            else TransferDisabled = true;
        }

        private bool IsValid(){
            Console.WriteLine("IsValid...");
            if(sender.Count>1) return false;
            if(sender.Count > 0 && sender[0].Balance < Sum) return false;
            if(TransferAmountNullable is null) return false;

            return true;
        }

        public void Transfer()
        {
            System.Console.WriteLine("transfer called...");
            if(!IsValid()){
                Status = "bg-danger";
                return;
            }
            if(sender.Count > 0) sender[0].Balance -= Sum;
            foreach (var r in receiver)
            {
                //r.Balance += TransferAmount;
                r.Balance += TransferAmountNullable??0;
            }
            Status = "bg-success";
            //TransferAmount = 0;
            //TransferAmountNullable = null;
            transferAmount = null;
            OnResetAll?.Invoke();
        }

    }
}