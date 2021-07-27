import { AuthorModel } from "./author.model";

export class BookModel{
    public id: string;
    public title: string;
    public description: number;    
    public publishDate?: Date;
    public authors: string;
    public authorsList: Array<AuthorModel> = [];
}