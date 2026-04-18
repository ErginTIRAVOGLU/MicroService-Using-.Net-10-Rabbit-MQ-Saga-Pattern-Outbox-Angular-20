import { Brand } from "./Brand";
import { Type } from "./Type";

export interface Product {
    id: string;
    name: string;
    description: string | null;
    imageFile: string;
    price: number;
    brand:{
        id:string;
        name:string;
    };
    type:{
        id:string;
        name:string;
    }
}