﻿using ICore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICore
{
    public interface IBill
    {
        /// <summary>
        /// The id of the Bill
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(36)]
        public string Id { get; }

        /// <summary>
        /// the name of the project for the bill
        /// </summary>
        public string ProjectName { get; }

        /// <summary>
        /// the start date of the project
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// the enddate of the project for the bill
        /// </summary>
        public DateTime EndDate { get; }

        /// <summary>
        /// the customer of the project for the bill
        /// </summary>
        public ICustomer ICustomer { get; }

        /// <summary>
        /// describes the id of the customer
        /// </summary>
        public string CustomerId { get; }

        /// <summary>
        /// the costs without taxes
        /// </summary>
        public double NetCost { get; }

        /// <summary>
        /// the costs with taxes
        /// </summary>
        public double TotalCost { get; }

        /// <summary>
        /// Project ID in which the article is used
        /// </summary>
        public string ProjectId { get; }
        //public IProject IProject { get; }

        /// <summary>
        /// the number of the Bill
        /// </summary>
        public static int BillNumber { get; }

        /// <summary>
        /// added articles in project
        /// </summary>
        public IEnumerable<IArticleHistory> IArticles { get; }

    }
}
