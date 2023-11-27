// See https://aka.ms/new-console-template for more information
using LineDirectionNeuralNetwork;

ImageReader reader = new ImageReader();
var grid = reader.Read();

Dictionary<string, double> data = new Dictionary<string, double>();

for(int row = 0; row < grid.Length - 1; row++)
{
    for(int col = 0; col < grid[row].Length - 1; col++)
    {
        NeuralNetwork neuralNetwork = new NeuralNetwork();

        NeuralNetworkLayer inputVector = new NeuralNetworkLayer();
        inputVector.AddNeuron(new Input(grid[row][col]));
        inputVector.AddNeuron(new Input(grid[row + 1][col]));
        inputVector.AddNeuron(new Input(grid[row][col + 1]));
        inputVector.AddNeuron(new Input(grid[row + 1][col + 1]));

        neuralNetwork.AddLayer(inputVector);

        NeuralNetworkLayer verticalComponent = new NeuralNetworkLayer(inputVector);
        verticalComponent.AddNeuron(new HyperbolicTangent(m => m.Map(0, 1.0).Map(1, 0.0).Map(2, 1.0).Map(3, 0.0)));
        verticalComponent.AddNeuron(new HyperbolicTangent(m => m.Map(0, 0.0).Map(1, 1.0).Map(2, 0.0).Map(3, 1.0)));
        verticalComponent.AddNeuron(new HyperbolicTangent(m => m.Map(0, 1.0).Map(1, 0.0).Map(2, -1.0).Map(3, 0.0)));
        verticalComponent.AddNeuron(new HyperbolicTangent(m => m.Map(0, 0.0).Map(1, 1.0).Map(2, 0.0).Map(3, -1.0)));
        neuralNetwork.AddLayer(verticalComponent);

        NeuralNetworkLayer horizontalComponent = new NeuralNetworkLayer(verticalComponent);
        horizontalComponent.AddNeuron(new HyperbolicTangent(m => m.Map(0, 1.0).Map(1, 1.0).Map(2, 0.0).Map(3, 0.0)));
        horizontalComponent.AddNeuron(new HyperbolicTangent(m => m.Map(0, -1.0).Map(1, 1.0).Map(2, 0.0).Map(3, 0.0)));
        horizontalComponent.AddNeuron(new HyperbolicTangent(m => m.Map(0, 0.0).Map(1, 0.0).Map(2, 1.0).Map(3, -1.0)));
        horizontalComponent.AddNeuron(new HyperbolicTangent(m => m.Map(0, 0.0).Map(1, 0.0).Map(2, 1.0).Map(3, 1.0)));
        neuralNetwork.AddLayer(horizontalComponent);

        NeuralNetworkLayer resolution = new NeuralNetworkLayer(horizontalComponent);
        resolution.AddNeuron(new RectifiedLinearUnit("irrelevant", m => m.Map(0, 1.0).Map(1, 0.0).Map(2, 0.0).Map(3, 0.0)));
        resolution.AddNeuron(new RectifiedLinearUnit("irrelevant", m => m.Map(0, -1.0).Map(1, 0.0).Map(2, 0.0).Map(3, 0.0)));
        resolution.AddNeuron(new RectifiedLinearUnit("vertical", m => m.Map(0, 0.0).Map(1, 1.0).Map(2, 0.0).Map(3, 0.0)));
        resolution.AddNeuron(new RectifiedLinearUnit("vertical", m => m.Map(0, 0.0).Map(1, -1.0).Map(2, 0.0).Map(3, 0.0)));
        resolution.AddNeuron(new RectifiedLinearUnit("diagonal", m => m.Map(0, 0.0).Map(1, 0.0).Map(2, 1.0).Map(3, 0.0)));
        resolution.AddNeuron(new RectifiedLinearUnit("diagonal", m => m.Map(0, 0.0).Map(1, 0.0).Map(2, -1.0).Map(3, 0.0)));
        resolution.AddNeuron(new RectifiedLinearUnit("horizontal", m => m.Map(0, 0.0).Map(1, 0.0).Map(2, 0.0).Map(3, 1.0)));
        resolution.AddNeuron(new RectifiedLinearUnit("horizontal", m => m.Map(0, 0.0).Map(1, 0.0).Map(2, 0.0).Map(3, -1.0)));
        neuralNetwork.AddLayer(resolution);

        foreach(var n in neuralNetwork.Run())
        {
            if (!data.ContainsKey(n.Item1))
                data.Add(n.Item1, 0);
            data[n.Item1] += n.Item2;
        }
    }
}

foreach(var entry in data)
{
    Console.WriteLine($"{entry.Key} - {entry.Value}");
}


//NeuralNetwork neuralNetwork = new NeuralNetwork();

//NeuralNetworkLayer inputVector = new NeuralNetworkLayer();
//inputVector.AddNeuron(new Input(grid[0][0]));
//inputVector.AddNeuron(new Input(grid[1][0]));
//inputVector.AddNeuron(new Input(grid[0][1]));
//inputVector.AddNeuron(new Input(grid[1][1]));

//for (int i = 0; i < grid.Length; i++)
//{
//    for (int j = 0; j < grid[i].Length; j++)
//    {
//        Console.WriteLine(grid[i][j]);
//        inputVector.AddNeuron(new Input(grid[i][j]));
//    }
//}




//inputVector.AddNeuron(new Input(0.5));
//inputVector.AddNeuron(new Input(0.0));
//inputVector.AddNeuron(new Input(0.75));
//inputVector.AddNeuron(new Input(-0.75));

//inputVector.AddNeuron(new Input(-1.0));
//inputVector.AddNeuron(new Input(1.0));
//inputVector.AddNeuron(new Input(-1.0));
//inputVector.AddNeuron(new Input(1.0));



Console.WriteLine();