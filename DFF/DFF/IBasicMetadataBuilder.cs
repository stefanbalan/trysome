namespace DFF;

public interface IBasicMetadataBuilder
{
    Task<Item> BuildMetadataAsync(Item item);
}