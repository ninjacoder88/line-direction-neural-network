namespace LineDirectionNeuralNetwork.Redux
{
    internal sealed class NeuralNetworkRedux
    {
        public NeuralNetworkRedux()
        {
            InputVector = new List<double>();
            _layers = new List<NeuralNetworkLayerRedux>();
        }

        public IReadOnlyList<double>? InputVector { get; private set; }

        public IReadOnlyList<NeuralNetworkLayerRedux> Layers => _layers;

        public int LayerCount => _layers.Count;

        public void AddLayer(List<NeuronRedux> neurons, List<Connection> connections)
        {
            _layers.Add(new NeuralNetworkLayerRedux(neurons, connections));
        }

        public void Run(List<double> inputVector)
        {
            InputVector = inputVector;
            Console.Write("InputVector: | ");
            foreach (var v in inputVector)
            {
                Console.Write(v.ToString() + " | ");
            }
            Console.WriteLine();

            List<double> data = inputVector;
            foreach(var layer in _layers)
            {
                data = layer.RunLayer(data);
                Console.Write("Layer      : | ");
                foreach (var d in data)
                {
                    Console.Write(d.ToString() + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private readonly List<NeuralNetworkLayerRedux> _layers;
    }

    internal sealed class Connection
    {
        public Connection(int inputNeuronIndex, int neuronIndex, double weight)
        {
            InputNeuronIndex = inputNeuronIndex;
            NeuronIndex = neuronIndex;
            Weight = weight;
        }

        public int NeuronIndex { get; }

        public int InputNeuronIndex { get; }

        public double Weight { get; }
    }

    internal sealed class NeuralNetworkLayerRedux
    {
        public NeuralNetworkLayerRedux(List<NeuronRedux> neurons, List<Connection> connections)
        {
            _neurons = neurons;
            _connections = connections;
        }

        public int LayerNumber { get; private set; }

        public IReadOnlyList<Connection> Connections => _connections;

        public IReadOnlyList<NeuronRedux> Neurons => _neurons;

        public List<double> RunLayer(List<double> data)
        {
            List<double> newData = new List<double>(_neurons.Count);

            for(int i = 0; i < _neurons.Count; i++)
            {
                var neuron = _neurons[i];
                var neuronConnections = _connections.Where(x => x.NeuronIndex == i);
                double total = 0;
                foreach(var connection in neuronConnections)
                {
                    total += data[connection.InputNeuronIndex] * connection.Weight;
                }
                neuron.Apply(total);
                newData.Add(neuron.Value);
            }
            return newData;
        }

        private readonly List<NeuronRedux> _neurons;
        private readonly List<Connection> _connections;
    }

    internal abstract class NeuronRedux
    {
        public double Value { get; protected set; }

        public string? Name { get; protected set; }

        public abstract void Apply(double total);
    }

    internal sealed class InputNeuronRedux : NeuronRedux
    {
        public InputNeuronRedux(double value)
        {
            Value = value;
        }

        public override void Apply(double total)
        { 
        }
    }

    internal sealed class HyperbolicTangentNeuronRedux : NeuronRedux
    {
        public override void Apply(double total)
        {
            Value = Math.Tanh(total);
        }
    }

    internal sealed class RectifiedLinearUnitNeuronRedux : NeuronRedux
    {
        public override void Apply(double total)
        {
            if(total < 0)
            {
                Value = 0;
                return;
            }
            Value = total;
        }
    }

    internal sealed class OutputNeuronRedux : NeuronRedux
    {
        public OutputNeuronRedux(string name)
        {
            Name = name;
        }

        public override void Apply(double total)
        {
            //if (Name == "solid")
            Value = total;
        }
    }
}
