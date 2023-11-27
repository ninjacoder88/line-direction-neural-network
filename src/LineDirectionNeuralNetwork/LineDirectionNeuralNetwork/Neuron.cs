namespace LineDirectionNeuralNetwork
{
    internal abstract class NeuralNetworkNeuron
    {
        public string Name { get; protected set; }

        public double Data { get; protected set; }

        internal List<NeuralNetworkNeuron> InputNeurons { get; private set; }

        public abstract void Apply();

        public virtual void SetInputNeurons(List<NeuralNetworkNeuron> neurons)
        {
            InputNeurons = neurons;
        }
    }

    internal sealed class Input : NeuralNetworkNeuron
    {
        public Input(double data)
        {
            Data = data;
        }

        public override void Apply()
        {
            
        }
    }

    internal sealed class Logistic : NeuralNetworkNeuron
    {
        public Logistic(Action<NeuronMapper> action)
        {
            _action = action;
        }

        public override void Apply()
        {
            NeuronMapper mapper = new NeuronMapper(InputNeurons);
            _action(mapper);
            Data = 1 / (1 + (Math.Pow(Math.E, mapper.Total * -1)));
        }

        private readonly Action<NeuronMapper> _action;
    }

    internal sealed class RectifiedLinearUnit : NeuralNetworkNeuron
    {
        public RectifiedLinearUnit(string name, Action<NeuronMapper> action)
        {
            Name = name;
            _action = action;
        }

        public override void Apply()
        {
            NeuronMapper mapper = new NeuronMapper(InputNeurons);
            _action(mapper);

            if(mapper.Total < 0)
            {
                Data = 0;
                return;
            }
            Data = mapper.Total;
        }

        private readonly Action<NeuronMapper> _action;
    }

    internal sealed class HyperbolicTangent : NeuralNetworkNeuron
    {
        public HyperbolicTangent(Action<NeuronMapper> action)
        {
            _action = action;
        }

        public override void Apply()
        {
            NeuronMapper mapper = new NeuronMapper(InputNeurons);
            _action(mapper);
            Data = Math.Tanh(mapper.Total);
        }

        private readonly Action<NeuronMapper> _action;
    }

    internal sealed class NeuronMapper
    {
        public NeuronMapper(List<NeuralNetworkNeuron> inputNeurons)
        {
            _inputNeurons = inputNeurons;
        }

        public double Total { get; private set; }

        public NeuronMapper Map(int index, double weight)
        {
            var n = _inputNeurons[index];
            var t = n.Data * weight;
            Total += t;
            return this;
        }

        private readonly List<NeuralNetworkNeuron> _inputNeurons;
    }
}
