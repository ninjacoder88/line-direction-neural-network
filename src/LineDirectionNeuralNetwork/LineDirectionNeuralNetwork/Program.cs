// See https://aka.ms/new-console-template for more information
using LineDirectionNeuralNetwork;
using LineDirectionNeuralNetwork.Redux;

NeuralNetworkRedux nn = new NeuralNetworkRedux();
nn.AddLayer(
    new List<NeuronRedux>
    {
        new HyperbolicTangentNeuronRedux(),
        new HyperbolicTangentNeuronRedux(),
        new HyperbolicTangentNeuronRedux(),
        new HyperbolicTangentNeuronRedux()
    },
    new List<Connection>
    {
        new Connection(0, 0, 1.0), new Connection(1, 0, 0.0), new Connection(2, 0, 1.0), new Connection(3, 0, 0.0),
        new Connection(0, 1, 0.0), new Connection(1, 1, 1.0), new Connection(2, 1, 0.0), new Connection(3, 1, 1.0),
        new Connection(0, 2, 1.0), new Connection(1, 2, 0.0), new Connection(2, 2, -1.0), new Connection(3, 2, 0.0),
        new Connection(0, 3, 0.0), new Connection(1, 3, -1.0), new Connection(2, 3, 0.0), new Connection(3, 3, 1.0),
    });
nn.AddLayer(new List<NeuronRedux>
    {
        new HyperbolicTangentNeuronRedux(),
        new HyperbolicTangentNeuronRedux(),
        new HyperbolicTangentNeuronRedux(),
        new HyperbolicTangentNeuronRedux()
    },
    new List<Connection>
    {
        new Connection(0, 0, 1.0), new Connection(1, 0, 1.0), new Connection(2, 0, 0.0), new Connection(3, 0, 0.0),
        new Connection(0, 1, 0.0), new Connection(1, 1, 0.0), new Connection(2, 1, 1.0), new Connection(3, 1, -1.0),
        new Connection(0, 2, 1.0), new Connection(1, 2, -1.0), new Connection(2, 2, 0.0), new Connection(3, 2, 0.0),
        new Connection(0, 3, 0.0), new Connection(1, 3, 0.0), new Connection(2, 3, 1.0), new Connection(3, 3, 1.0),
    });
nn.AddLayer(new List<NeuronRedux>
    {
        new RectifiedLinearUnitNeuronRedux(),
        new RectifiedLinearUnitNeuronRedux(),
        new RectifiedLinearUnitNeuronRedux(),
        new RectifiedLinearUnitNeuronRedux(),
        new RectifiedLinearUnitNeuronRedux(),
        new RectifiedLinearUnitNeuronRedux(),
        new RectifiedLinearUnitNeuronRedux(),
        new RectifiedLinearUnitNeuronRedux(),
    },
    new List<Connection>
    {
        new Connection(0, 0, 1.0), new Connection(0, 1, -1.0),
        new Connection(1, 2, 1.0), new Connection(1, 3, -1.0),
        new Connection(2, 4, 1.0), new Connection(2, 5, -1.0),
        new Connection(3, 6, 1.0), new Connection(3, 7, -1.0),
    });
nn.AddLayer(
    new List<NeuronRedux>
    {
        new OutputNeuronRedux("solid"),
        new OutputNeuronRedux("horizontal"),
        new OutputNeuronRedux("vertical"),
        new OutputNeuronRedux("diagonal")
    },
    new List<Connection>
    {
        new Connection(0, 0, 1), new Connection(1, 0, 1),
        new Connection(2, 1, 1), new Connection(3, 1, 1),
        new Connection(4, 2, 1), new Connection(5, 2, 1),
        new Connection(6, 3, 1), new Connection(7, 3, 1)
    });

//nn.Run(new List<double> { 1, -1, -1, 1 });

ImageReader reader = new ImageReader();
var grid = reader.Read();
for (int row = 0; row < grid.Length - 1; row++)
{
    for (int col = 0; col < grid[row].Length - 1; col++)
    {
        nn.Run(new List<double> {grid[row][col], grid[row][col+1], grid[row+1][col], grid[row+1][col+1] });
    }
}