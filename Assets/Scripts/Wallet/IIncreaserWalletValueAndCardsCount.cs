using Data;

public interface IIncreaserWalletValueAndCardsCount
{
    public void AccrueCard(CardData card, int count);

    public void AccrueCristal(int amountCristal);

    public void AccrueGold(int amountGold);

    public void AccrueBottle(ShopItemBottle bottle, int amountBottle);

}