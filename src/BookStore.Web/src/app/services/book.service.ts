import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BookModel } from "../models/book.model";
import { Page, PageRequest } from "../models/pagination.model";
import { ApiService } from "./api.service";

@Injectable({
    providedIn: 'root'
  })  
  export class BookService extends ApiService {   

    constructor(protected  http: HttpClient) {
      super(http, 'Book')
    }
    
    public listBooks(pageRequest?: PageRequest): Observable<Page<BookModel>>{
      return this.list(pageRequest);
    }

    public listBooksDataTable(dataTableParameters: any): Observable<Page<BookModel>>{
      return this.listDataTable(dataTableParameters, 'list');
    }

    public getBook(id: string): Observable<BookModel> {
      return this.get(id);
    }

    public saveBook(book: BookModel, isEdit:boolean = false){
      if(isEdit){
        return this.put(book.id, book);
      }
      return this.post(book);
    }

  }