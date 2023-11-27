namespace LineDirectionNeuralNetwork
{
    internal sealed class NeuralNetworkLayer
    {
        public NeuralNetworkLayer(NeuralNetworkLayer previousLayer)
            : this()
        {
            _previousLayer = previousLayer;
        }

        public NeuralNetworkLayer()
        {
            _neurons = new List<NeuralNetworkNeuron>();
        }

        public List<NeuralNetworkNeuron> Neurons => _neurons;

        public void AddNeuron(NeuralNetworkNeuron neuron)
        {
            if (_previousLayer != null)
                neuron.SetInputNeurons(_previousLayer._neurons);
            _neurons.Add(neuron);
        }

        public void Construct()
        {
            foreach (var neuron in _neurons)
            {
                neuron.Apply();
                //Console.WriteLine(neuron.Data);
            }
            //return _neurons;
        }

        private readonly List<NeuralNetworkNeuron> _neurons;
        private readonly NeuralNetworkLayer? _previousLayer;
    }
}
