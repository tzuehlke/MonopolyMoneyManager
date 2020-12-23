namespace MonopolyMoneyManager.BusinessController
{
    using System;
    using System.Collections.Generic;
    using MonopolyMoneyManager.BusinessObjects;

    public class TransferController {
        public bool TransferDisabled { get; set; } = false;
        public string Msg { get; set; } = "";
        public string Status { get; set; } = "bg-info";
        public int Sum { get { return (receiver.Count == 0 ? TransferAmount : receiver.Count * TransferAmount) ;} }
        private int transferAmount; 
        public int TransferAmount {
            get{ return transferAmount;}
            set{
                transferAmount = value;
                UpdateGui();
            }
        }

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
            Console.WriteLine("ChangeSender...");
            if(g.IsSender) sender.Add(g.Gamer);
            else sender.Remove(g.Gamer);
            UpdateGui();
        }
        public void ChangeReceiver(GamerController g){
            Console.WriteLine("ChangeReceiver...");
            if(g.IsReceiver) receiver.Add(g.Gamer);
            else receiver.Remove(g.Gamer);
            UpdateGui();
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
            Msg = msg_source + "send " + Sum.ToString("C") + " to " + msg_dest;
        }

        private void SetTransferEnabled(){
            if(IsValid()) TransferDisabled = false;
            else TransferDisabled = true;
        }

        private bool IsValid(){
            Console.WriteLine("IsValid...");
            if(sender.Count>1) return false;
            if(sender.Count > 0 && sender[0].Balance < Sum) return false;

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
                r.Balance += TransferAmount;
            }
            Status = "bg-success";
        }

    }
}