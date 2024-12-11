import {nanoid}from 'nanoid';
export type Cart_Type ={
 id: string;
 items: CartItem[]
}

export type CartItem ={
 productId: number;
 productName: string;
 price: number;
 quantity: number;
 pictureUrl: string;
 brand: string;
 type: string;
}

export class Cart implements Cart_Type{
id = nanoid();
items: CartItem[]=[];
}