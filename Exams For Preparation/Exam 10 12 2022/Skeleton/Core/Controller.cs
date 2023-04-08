using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths;

        private string[] availableTypes = { "Gingerbread", "Stolen" };
        private string[] availableTypesCocktail = { "Hibernation", "MulledWine" };
        private string[] sizes = { "Large", "Middle", "Small" };

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            IBooth booth = new Booth(booths.Models.Count + 1, capacity);

            booths.AddModel(booth);

            return String.Format(OutputMessages.NewBoothAdded, booth.BoothId, capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (!availableTypesCocktail.Contains(cocktailTypeName))
            {
                return String.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            if (!sizes.Contains(size))
            {
                return String.Format(OutputMessages.InvalidCocktailSize, size);
            }

            if (booths.Models.Any(b => b.CocktailMenu.Models.Any(cm => cm.Name == cocktailName && cm.Size == size)))
            {
                return String.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            ICocktail cocktail;
            if (cocktailTypeName == "Hibernation")
            {
                cocktail = new Hibernation(cocktailName, size);
            }
            else
            {
                cocktail = new MulledWine(cocktailName, size);
            }

            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.CocktailMenu.AddModel(cocktail);

            return String.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (!availableTypes.Contains(delicacyTypeName))
            {
                return String.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if (booths.Models.Any(b => b.DelicacyMenu.Models.Any(dn => dn.Name == delicacyName)))
            {
                return String.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            IDelicacy delicacy;
            if (delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else
            {
                delicacy = new Stolen(delicacyName);
            }

            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.DelicacyMenu.AddModel(delicacy);

            return String.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
        }

        public string BoothReport(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            

            return booth.ToString();
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(s => s.BoothId == boothId);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format(OutputMessages.GetBill, $"{booth.CurrentBill:f2}"));

            booth.Charge();
            booth.ChangeStatus();

            sb.AppendLine(String.Format(OutputMessages.BoothIsAvailable, boothId));

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            var ordered = booths.Models
                .Where(s => s.IsReserved == false && s.Capacity >= countOfPeople)
                .OrderBy(s => s.Capacity)
                .ThenByDescending(s => s.BoothId);

            var first = ordered.FirstOrDefault();

            if (first == null)
            {
                return String.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }
            else
            {
                first.ChangeStatus();
                return String.Format(OutputMessages.BoothReservedSuccessfully, first.BoothId, countOfPeople);
            }

        }

        public string TryOrder(int boothId, string order)
        {
            throw new NotImplementedException();
        }
    }
}
