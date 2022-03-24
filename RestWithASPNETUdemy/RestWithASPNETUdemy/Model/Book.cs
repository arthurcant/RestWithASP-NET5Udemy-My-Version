using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("book")]
    public class Book
    {

//    id` INT(10) AUTO_INCREMENT PRIMARY KEY,
//   `author` longtext,
//   `launch_date` datetime(6) NOT NULL,
//   `price` decimal(65,2) NOT NULL,
//   `title` longtext

    [Column("id")]
    public int Id {get; set;}

    [Column("author")]
    public string Author {get; set;}

    [Column("launch_date")]
    public DateTime launchDate {get; set;}

    [Column("price")]
    public decimal Price {get; set;}

    [Column("title")]
    public string Title {get; set;}




    }
}