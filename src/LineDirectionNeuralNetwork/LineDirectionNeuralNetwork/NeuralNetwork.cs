namespace LineDirectionNeuralNetwork
{
    internal sealed class NeuralNetwork
    {
        public NeuralNetwork()
        {
            _layers = new List<NeuralNetworkLayer>();
        }

        public void AddLayer(NeuralNetworkLayer layer)
        {
            _layers.Add(layer);
        }

        public IEnumerable<Tuple<string, double>> Run()
        {
            NeuralNetworkLayer lastLayer = new NeuralNetworkLayer();

            for (int l = 0; l < _layers.Count; l++)
            {
                var layer = _layers[l];
                layer.Construct();
                lastLayer = layer;
            }

            

            var list = lastLayer.Neurons.OrderByDescending(x => x.Data).ToList();
            foreach (var item in list)
            {
                yield return new Tuple<string, double>(item.Name, item.Data);
                //Console.WriteLine($"{item.Name} - {item.Data}");
            }
            //Console.WriteLine();

            //return lastLayer.Neurons;

            //var highest = lastLayer.Neurons.GroupBy(x => x.Data).OrderByDescending(x => x.Key).First();
            //if (highest.Count() > 1)
            //    return "unknown";

            //return highest.First().Name;


            //foreach(var layer in _layers)
            //{
            //    var neurons = layer.Construct();
            //    Console.WriteLine();
            //}
        }

        private readonly List<NeuralNetworkLayer> _layers;
    }
}
