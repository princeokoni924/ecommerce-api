import {nanoid}from 'nanoid';
export type CartType ={
 id: string;
 items: Cart_Item[];
 deliveryMethodId?: number;
 paymentIntentId?: string;
 clientSecret?: string;
}

export type Cart_Item = {
 productId: number;
 productName: string;
 price: number;
 quantity: number;
 pictureUrl: string;
 brand: string;
 productType: string;
}

export class Cart implements CartType{
id = nanoid();
items: Cart_Item[]=[];
deliveryMethodId?: number;
paymentIntentId?: string;
clientSecret?: string;

}