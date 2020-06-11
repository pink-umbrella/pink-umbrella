using System;
using System.Collections.Generic;
using seattle.Models;

namespace seattle.Services
{
    public class TestSimpleResourceService: ISimpleResourceService
    {
        private static Random rand = new Random();
        private static string[] Brands = { "Unicorn", "Rainbow", "Hot Cakes", "LaLaLatte", "The People's" };
        private static string[] Categories = { "Food", "Snacks", "Water", "Beverage", "Sanitation", "Medical", "Clothing", "Defense", "Bathroom", "Safe Space" };
        private static Dictionary<string, string[]> CategoryItems = new Dictionary<string, string[]> { 
            { "Food", new string[] { "Hot Dogs", "Pizza", "Curry Rice Balls", "Pickles" } },
            { "Snacks", new string[] { "Fruit Snacks", "Granola Bars", "Raisins", "Nuts" } },
            { "Water", new string[] { "Water Bottles", "Cups of Water" } },
            { "Beverage", new string[] { "Soda", "Juice", "Coffee", "Tea", "Cold Brew" } },
            { "Sanitation", new string[] { "Personal Hand Sanitizer", "Communal Hand Sanitizer" } },
            { "Medical", new string[] { "Band Aids", "First Aid Kit" } },
            { "Clothing", new string[] { "Shirts", "Socks", "Back Packs" } },
            { "Defense", new string[] { "Shields", "Umbrellas", "Traffic Cones", "Barricade" } },
            { "Bathroom", new string[] { "Honey Bucket", "Public Bathroom", "Private Bathroom" } },
            { "Safe Space", new string[] { "Public Space", "Private Space" } }
        };
        
        private static Dictionary<string, string> ItemDescriptions = new Dictionary<string, string> { 
            { "Hot Dogs", "Delicious meat tubes" },
            { "Pizza", "Delicious flat cheese bread" },
            { "Curwry Rice Balls", "Delicious savory balls" },
            { "Pickles", "Delicious vinegar crunch" },
            { "Fruit Snacks", "Gummy and sweet" },
            { "Granola Bars", "Chocolate and boneless" },
            { "Raisins", "I hear these are made of dried grapes" },
            { "Nuts", "Delicious and they stay good for a long time" },
            { "Water Bottles", "Water you can take with you without worrying about it spilling" },
            { "Cups of Water", "Water in a cup" },
            { "Soda", "Bubbly sugar water, comes in many flavors" },
            { "Juice", "Sugar water from plants" },
            { "Coffee", "Wasn't there a book about stimulants being readily available?" },
            { "Tea", "We dumped this into a bay at one point" },
            { "Cold Brew", "A different experience from hot brewed coffee" },
            { "Personal Hand Sanitizer", "Hand sanitizer in a small container you can carry with yourself" },
            { "Communal Hand Sanitizer", "Hand sanitizer shared by the community" },
            { "Band Aids", "Bandages you can apply to small cuts" },
            { "First Aid Kit", "A collection of various medical supplies" },
            { "Shirts", "Clothing for your top half" },
            { "Socks", "Clothing for your lower half" },
            { "Back Packs", "Clothing to hold stuff" },
            { "Shields", "Flat reinforced equipment you can hold to defend yourself" },
            { "Umbrellas", "In most places this protects from rain, but in Seattle it protects from riot police" },
            { "Traffic Cones", "Used to diffuse tear gas" },
            { "Barricade", "Protects people from vehicles" },
            { "Honey Bucket", "Provides a place for people to relieve themselves anywhere" },
            { "Public Bathroom", "A proper bathroom in a building, typically a restaraunt" },
            { "Private Bathroom", "A proper bathroom in a building, typically a house or apartment" },
            { "Public Space", "Typically a restaraunt or library" },
            { "Private Space", "Typically a house or apartment" }
        };
        
        private static Dictionary<string, string> ItemUnits = new Dictionary<string, string> { 
            { "Hot Dogs", "Dogs" },
            { "Pizza", "Slices" },
            { "Curry Rice Balls", "Balls" },
            { "Pickles", "Pickles" },
            { "Granola Bars", "Bars" },
            { "Raisins", "Boxes" },
            { "Water Bottles", "Bottles" },
            { "Cups of Water", "Cups" },
            { "Soda", "Bottles" },
            { "Juice", "Bottles" },
            { "Coffee", "Cups" },
            { "Tea", "Cups" },
            { "Cold Brew", "Cups" },
            { "Personal Hand Sanitizer", "Bottles" },
        };

        private static int NextId = 1;

        private static List<SimpleResourceModel> TestItems = null;

        public List<SimpleResourceModel> GetTestResources() {
            if (TestItems == null) {
                TestItems = new List<SimpleResourceModel>();
                for (int i = 0; i < 1000; i++) {
                    TestItems.Add(RandomTestResource());
                }
            }
            return TestItems;
        }

        private SimpleResourceModel RandomTestResource()
        {
            var category = RandomCategory();
            var name = NameForCategory(category);
            return new SimpleResourceModel() {
                id = NextId++,
                Brand = RandomBrand(),
                Category = category,
                Name = name,
                Description = DescriptionForCategoryAndName(category, name),
                Units = UnitsForCategoryAndName(category, name),
                Amount = AmountForCategoryAndName(category, name),
                forked_from_id = 0,
                created_by_user_id = 0,
                inventory_id = 0,
                mipmap_id = 0,
            };
        }

        private string SelectOne(string[] items) { return items[(int)Math.Floor(rand.NextDouble() * items.Length)]; }

        private string RandomBrand() { return SelectOne(Brands); }

        private string RandomCategory() { return SelectOne(Categories); }

        private string NameForCategory(string category) { return SelectOne(CategoryItems[category]); }

        private double AmountForCategoryAndName(string category, string name) { return rand.NextDouble() * 4 + 1; }

        private string UnitsForCategoryAndName(string category, string name) { return ItemUnits.TryGetValue(name, out var unit) ? unit : "Unit"; }

        private string DescriptionForCategoryAndName(string category, string name) { return ItemUnits.TryGetValue(name, out var unit) ? unit : "Default description"; }

        public List<SimpleResourceModel> GetAllForUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<SimpleResourceModel> QueryInventory(int id, string text, PaginationModel pagination)
        {
            throw new NotImplementedException();
        }

        public List<SimpleResourceModel> QueryAll(string text, PaginationModel pagination)
        {
            throw new NotImplementedException();
        }

        public SimpleResourceModel GetResource(int id)
        {
            throw new NotImplementedException();
        }

        public SimpleResourceModel CreateResource(SimpleResourceModel initial)
        {
            throw new NotImplementedException();
        }

        public SimpleResourceModel ForkResource(int id, int inventoryId)
        {
            throw new NotImplementedException();
        }

        public void UpdateAmount(int id, double newAmount)
        {
            throw new NotImplementedException();
        }

        public void DeleteResource(int id, int by_user_id)
        {
            throw new NotImplementedException();
        }
    }
}