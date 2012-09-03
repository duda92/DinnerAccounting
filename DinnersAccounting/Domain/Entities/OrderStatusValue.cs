namespace DA.Dinners.Domain
{
    /// <summary>
    /// Status of an order
    /// </summary>
    public enum OrderStatusValue : int
    {
        Order = 1,
        SentOrder = 2,
        DeliveredOrder = 3,
        NotDeliveredOrder = 4,
        Payed = 5,
    }
}