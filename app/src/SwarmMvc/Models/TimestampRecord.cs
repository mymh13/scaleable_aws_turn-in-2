using Amazon.DynamoDBv2.DataModel;

namespace SwarmMvc.Models;

[DynamoDBTable("swarmcf-Submissions")]
public class TimestampRecord
{
    [DynamoDBHashKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [DynamoDBProperty]
    public DateTime Timestamp { get; set; }

    [DynamoDBProperty]
    public string ContainerInfo { get; set; } = string.Empty;

    [DynamoDBProperty]
    public string Status { get; set; } = "Recorded";
}