namespace MonopolyMoneyManager.Pages
{
using System.Collections.Generic;
using System.Threading.Tasks;
//using System.Text;
using System;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using TestBlazorWebAss.BusinessController;
using MonopolyMoneyManager.BusinessController;
using MonopolyMoneyManager.BusinessObjects;

    public partial class Monopoly
    {
        protected override async Task OnInitializedAsync()
        {
            init();

        }
        TransferController transferController = new TransferController();
        IList<GamerController> GamerControllers = new List<GamerController>();
        public void init(){
            GamerControllers.Add(new GamerController(new Gamer(){Name = "Pott", Balance=0}));
            GamerControllers.Add(new GamerController(new Gamer(){Name = "Gamer 1", Balance=100}));
            GamerControllers.Add(new GamerController(new Gamer(){Name = "Gamer 2", Balance=100}));
            GamerControllers.Add(new GamerController(new Gamer(){Name = "Gamer 3", Balance=100}));
            GamerControllers.Add(new GamerController(new Gamer(){Name = "Gamer 4", Balance=100}));

            foreach (var g in GamerControllers)
            {
                g.OnSenderChanged += () => transferController.ChangeSender(g);
                g.OnReceiverChanged += ()=> transferController.ChangeReceiver(g);
            }
            transferController.UpdateGui();
        }

    }

}