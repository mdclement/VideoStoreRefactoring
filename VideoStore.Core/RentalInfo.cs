using System;
using System.Collections.Generic;

namespace VideoStore.Core
{
    public class RentalInfo
    {
        public string Statement()
        {
            var customer = GetCurrentCustomer();
            var totalAmount = 0m;
            var frequentRenterPoints = 0;
            var result = "Rental Record for " + customer.Name + "\n";
            foreach (var r in customer.Rentals)
            {
                var movie = LookupMovie(r.MovieId);
                var thisAmount = 0m;

                // determine amount for each movie
                switch (movie.Code)
                {
                    case "regular":
                    {
                        thisAmount = 2m;
                        if (r.Days > 2)
                        {
                            thisAmount += (r.Days - 2) * 1.5m;
                        }
                    }
                        break;
                    case "new":
                        thisAmount = r.Days * 3;
                        break;
                    case "childrens":
                        thisAmount = 1.5m;
                        if (r.Days > 3)
                        {
                            thisAmount += (r.Days - 3) * 1.5m;
                        }

                        break;
                }

                //add frequent renter points
                frequentRenterPoints++;
                // add bonus for a two day new release rental
                if (movie.Code == "new" && r.Days > 2) frequentRenterPoints++;

                //print figures for this rental
                result += "\t" + movie.Title + "\t" + thisAmount + "\n";
                totalAmount += thisAmount;
            }

            // add footer lines
            result += "Amount owed is " + totalAmount + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points\n";

            return result;
        }

        private Movie LookupMovie(string id)
        {
            var movies = new Dictionary<string, Movie>
            {
                {"F001", new Movie("Ran", "regular")},
                {"F002", new Movie("Trois Couleurs: Bleu", "regular")},
                {"F003", new Movie("Cars 2", "childrens")},
                {"F004", new Movie("Latest Hit Release", "new")}
            };
            return movies[id];
        }

        private static Customer GetCurrentCustomer()
        {
            var rand = new Random();
            return rand.NextDouble() > .5
                ? new Customer("martin", new List<MovieRental>
                {
                    new MovieRental("F001", 3),
                    new MovieRental("F002", 1)
                })
                : new Customer("martin", new List<MovieRental>
                {
                    new MovieRental("F003", 3),
                    new MovieRental("F004", 1)
                });
        }
    }

    public class Movie
    {
        public Movie(string title, string code)
        {
            Title = title;
            Code = code;
        }

        public string Code { get; }
        public string Title { get; }
    }

    public class Customer
    {
        public Customer(string name, List<MovieRental> rentals)
        {
            Name = name;
            Rentals = rentals;
        }

        public string Name { get; }
        public List<MovieRental> Rentals { get; }
    }

    public class MovieRental
    {
        public MovieRental(string movieId, int days)
        {
            MovieId = movieId;
            Days = days;
        }
        public string MovieId { get; }
        public int Days { get; }
    }
}
