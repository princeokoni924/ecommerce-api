import {nanoid}from 'nanoid';
export type Cart_Type ={
 id: string;
 items: Cart_Item[];
}

export type Cart_Item = {
 productId: number;
 productName: string;
 price: number;
 quantity: number;
 pictureUrl: string;
 brand: string;
 productType: string;
 //type: string;
}

export class Cart implements Cart_Type{
id = nanoid();
items: Cart_Item[]=[];

}