using System;
using System.Collections.Generic;
using System.Text;

using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int id;
        private int capacity;
        private double currBill;
        private double turnover;

        private readonly IRepository<IDelicacy> delicacies;
        private readonly IRepository<ICocktail> cocktails;

        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;

            currBill = 0;
            turnover = 0;

            delicacies = new DelicacyRepository();
            cocktails = new CocktailRepository();
        }

        public int BoothId
        {
            get { return id; }
            private set { id = value; }
        }

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                }

                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => this.delicacies;

        public IRepository<ICocktail> CocktailMenu => this.cocktails;

        public double CurrentBill => this.currBill;

        public double Turnover => this.turnover;

        public bool IsReserved { get; private set; }

        public void ChangeStatus()
        {
            if (IsReserved)
            {
                IsReserved = false;
                return;
            }

            IsReserved = true;
        }

        public void Charge()
        {
            this.turnover += CurrentBill;
            this.currBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            this.currBill += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine($"-Cocktail menu:");
            foreach (var item in CocktailMenu.Models)
            {
                sb.AppendLine($"--{item}");
            }
            sb.AppendLine($"-Delicacy menu:");
            foreach (var item in DelicacyMenu.Models)
            {
                sb.AppendLine($"--{item}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
