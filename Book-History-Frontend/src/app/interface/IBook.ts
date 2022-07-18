import { IAuthor } from "./IAuthor";

export interface IBook {
    id: number;
    title: string;
    description: string;
    publishDate: Date;
    authors:IAuthor[]
  }
  
  