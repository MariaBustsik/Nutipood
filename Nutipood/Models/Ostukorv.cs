using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nutipood.Models
{
    public class Ostukorv
    {

        private List<SmartDevice> content;
        public Ostukorv()
        {
            this.content = new List<SmartDevice>();
        }
        public List<SmartDevice> GetContent() { return content; }
        public SmartDevice GetProductById(int id)
        {
            return this.content.Find(p => p.Id == id);
        }
        public void AddProduct(SmartDevice product)
        {
            this.content.Add(product);
        }

        public void RemoveProduct(int id)
        {
            SmartDevice product = this.content.Find(p => p.Id == id);
            this.content.Remove(product);
        }

        public void ClearOstukorv()
        {
            this.content.Clear();
        }
        public int CountOstukorv()
        {
            return this.content.Count;
        }
        public double GetTotal()
        {
            return this.content.Sum(p => p.Hind);
        }
        public List<string> GetProductNames()
        {
            List<string> list = new List<string>();
            content.ForEach(p =>
            {
                list.Add(p.Model);
            });

            return list;
        }

        //метод delete с объектом SmartDevice в качестве параметра
        public void RemoveProduct(SmartDevice product)
        {
            this.content.Remove(product);
        }





    }
}
