using System;
using System.Collections.Generic;


/*
Console-based Amazon
View a list of 5-6 products: should include their name, description, and price
Create a customer account and my customer account should have my name, my address, my delivery preferences, and a list of all the items I have purchased
Set quanities of items, can't buy items that the store is out of
*/


class Program {
  public static void Main (string[] args) {
    
    Console.WriteLine ("");
    
    List<Product> productList = new List<Product>();
    List<Customer> customerList = new List<Customer>();
    List<Product> customerPurchases = new List<Product>();

//Products in productList
    
    Product apples = new Product(0, "Apples", "12-pack in plastic container", 7.40, 27, 0);
    productList.Add(apples);

    Product bananas = new Product(0, "Bananas", "4-9 bundled together", 1.99, 5, 0);
    productList.Add(bananas);

    Product cherries = new Product(0, "Cherries", "14-20 in titanium container", 4.32, 0, 0);
    productList.Add(cherries);

    Product granolaBar = new Product(0, "Granola Bar", "50 in cardboard box", 10.99, 62, 0);
    productList.Add(granolaBar);

    Product boxOfTeddyBears = new Product(0, "Teddy Bear Surprise", "10 teddy bears, varying in size", 0.01, 1043, 0);
    productList.Add(boxOfTeddyBears);

//Customers in customerList

    Customer drew = new Customer("Drew", "123 Way Way", "Friendly");
    customerList.Add(drew);

    Customer lexi = new Customer("Lexi", "239487 Staple Street", "No Preferences");
    customerList.Add(lexi);

    Customer bob = new Customer("Bob", "789 Limecastle Lane", "Super Prime Membership");
    customerList.Add(bob);

//Purchases in customerPurchases

    Product applez = new Product(0, "Apples", "12-pack in plastic container", 7.40, 0, 50000);
    customerPurchases.Add(applez);

    
    
    Console.WriteLine("Welcome to our BRAND NEW Ecommerce webpage! What are you looking for? What do you need?\nPlease select an option using the provided key below: \n" 
                      + "CA - Create Account \n"
                      + "PH - Purchase History \n"
                      + "AP - Available Products \n"
                      + "E - Exit \n");

    string userChoice = Console.ReadLine();
    Console.WriteLine("");

    if(userChoice == "CA") //CREATE ACCOUNT
    {
      Console.WriteLine("To create a new account, please provide the information asked.\nYour name:");
      string userName = Console.ReadLine();
      
      Console.WriteLine("\nYour address:");
      string userAddress = Console.ReadLine();

      Console.WriteLine("\nWhat are your delivery preferences? Choose from the following options: \n"
                      + "Super Prime Membership - delivers within the next 10 minutes \n"
                      + "By-The-Hour - delivers within the next hour \n"
                      + "Friendly - your deliverer will try to engage in a long, uncomfortable converstion as they carry your groceries to the door \n"
                      + "No Preferences - n/a\n");
      string preference = Console.ReadLine();

      Customer newCustomer = new Customer(userName, userAddress, preference);
      customerList.Add(newCustomer);
      Console.WriteLine("\nYour account has been created!");
    }
    
    else if(userChoice == "PH") //PURCHASE HISTORY
    {
      foreach(Product purchased in customerPurchases)
      {
        Console.WriteLine(purchased.name + "\n");
      }
    }
    
    else if(userChoice == "AP") //AVAILABLE PRODUCTS
    {
      bool areYouDone = false;
      
      while(areYouDone == false)
      {
        Console.WriteLine("Using the provided number key, select items you wish to add to your cart.");
        int count = 1;
        foreach(Product available in productList)
        {
          if(available.inStock > 0)
          {
            Console.WriteLine(count + " - " + available.name + ", " + available.inStock + " in stock");
            available.displayIndex = count;
            count++;
          }
        }
        Console.WriteLine("E - " + "Exit 'Add to Cart'\n");
  
        string putInCartInput = Console.ReadLine();

        if(putInCartInput == "E")
        {
          Console.WriteLine("\nExiting 'Available Products'...");
          break;
        }

        int putInCart = int.Parse(putInCartInput);
          
        Console.WriteLine("\nHow many of this item do you wish to take? Please input numbers ONLY.\n");
  
        string take = Console.ReadLine();
        Console.WriteLine("");
        int taking = int.Parse(take);
        
        if(putInCart > 0 && putInCart < productList.Count)
        {
          //int index = 1;
          foreach(Product available1 in productList)
          {
            if(putInCart == available1.displayIndex)
            {
              Console.WriteLine("The combined total of " + taking + " " + available1.name + " is $" + (taking*available1.price) + "\n");
              Console.WriteLine("Do you still wish to purchase this item?\n"
                                + "y - Yes\n"
                                + "n - No\n");
              string userReply = Console.ReadLine();
              if(userReply == "y")
              {
                customerPurchases.Add(available1);
                Console.WriteLine("\n" + taking + " " + available1.name + " have been added to your cart!");
                if(taking <= available1.inStock)
                {
                  available1.inStock -= taking;
                }
                break;
              }
            }
            //index++;
          }
        }
        else
        {
          Console.WriteLine("The value you entered to take was over our stock limit. Please select 'Yes' in the next question to cycle through and try again.\n");
        }
  
        Console.WriteLine("\nDo you wish to continue adding items?\n"
                         + "y - Yes\n"
                         + "n - No\n");
        string userAnswer = Console.ReadLine();
        Console.WriteLine("");
  
        if(userAnswer == "n")
        {
          areYouDone = true;
        }
        else if(userAnswer == "y")
        {
          areYouDone = false;
        }
        else
        {
          Console.WriteLine("\nSelection did not correspond to 'Yes' or 'No'. Try again.");
        }
      }
    }
      
    else if(userChoice == "E") //EXIT
    {
      Console.WriteLine("");
    }
    
    else //WRONG KEY CATCH ALL
    {
      Console.WriteLine("\nNot an option. Please rerun the program and choose from the available menu.");
    }

    Console.WriteLine("\nThank you for shopping!");
  }
}



class Product
{
  public int displayIndex;
  public string name;
  public string description;
  public double price;
  public int inStock;
  public int bought;

  public Product(int dI, string n, string d, double p, int iS, int b)
  {
    displayIndex = dI;
    name = n;
    description = d;
    price = p;
    inStock = iS;
    bought = b;
  }

  public void productInfo()
  {
    Console.WriteLine("Product Name: " + name + "\n"
                     + "Description: " + description + "\n"
                     + "Price: $" + price);
  }
}



class Customer
{
  public string name;
  public string address;
  public string delPref;
  //public string[] customerPurchases;
  //List<Product> customerPurchases = new List<Product>();

  public Customer(string n, string a, string dP)
  {
    name = n;
    address = a;
    delPref = dP;
  }

  /*
  public void customertInfo(string person)
  {
    foreach(Customer account in customerList)
    {
      
    }
    Console.WriteLine("Name: " + name + "\n"
                     + "Address: " + address + "\n"
                     + "Delivery Preferences: " + delPref + "\n"
                     + "Purchases: " + purchases);
  }
  */
}