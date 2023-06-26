using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Dashboard
    {
        public List<string> labels { get; private set; }
        public List<Dataset> datasets { get; set; }

        public void AddLabel(string init, string end)
        {
            int mounthInit = Convert.ToInt32(init.Split("/")[0]);
            int yearInit = Convert.ToInt32(init.Split("/")[1]);
            int mounthEnd = Convert.ToInt32(end.Split("/")[0]);
            int yearEnd = Convert.ToInt32(end.Split("/")[1]);
            labels = new List<string>();
            datasets = new List<Dataset>();
            do
            {
                if (mounthInit > 12)
                {
                    mounthInit = 1;
                    yearInit++;
                }
                labels.Add(string.Format("{0}/{1}", mounthInit.ToString("00"),yearInit.ToString("0000")));
            } while (!(mounthInit++ == mounthEnd && yearInit == yearEnd));
        }
        public class Dataset
        {
            public string label { get; set; }
            public List<int> data { get; set; }
        }
        public void AddDataSet(string label,int data)
        {
            if(datasets.Exists(d => d.label == label)){
                datasets[datasets.IndexOf(datasets.First(d => d.label == label))].data.Add(data);
                return;
            }
            Dataset dataset = new Dataset();
            dataset.data = new List<int>();
            dataset.data.Add(data);
            dataset.label = label;
            datasets.Add(dataset);
        }
    }
}
