import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { BookModel } from '../models/book.model';
import { BookService } from '../services/book.service';

declare var $: any;

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit {

  dtTrigger: Subject<any> = new Subject<any>();
  public dtOptions: any = {};

  constructor(private router: Router, private bookService: BookService, private datePipe: DatePipe) { }

  public books: Array<BookModel>;

  ngOnInit(): void {

    this.dtOptions = {
      pagingType: "simple_numbers",
      pageLength: 20,
      serverSide: true,
      processing: true,
      ajax: (dataTableParameters: any, callback) => {
        this.bookService.listBookDataTable(dataTableParameters).subscribe(page => {
          this.books = page.data;
          callback({
            recordsTotal: page.recordsTotal,
            recordsFiltered: page.recordsFiltered,
            data: page.data
          });
        })
      },
      columns: [
        { data: "title", name: "Title" },
        { data: "description", name: "Description" },
        { data: "publishDate", name: "Publish Date", searchable: false, ngPipeInstance : this.datePipe },
        { data: "authors", name: "Authors", sortable: true, searchable: false },
      ],
      dom: '<fB<t>ip>',
      rowCallback: (row: Node, data: BookModel, index: number) => {
        const self = this;
        $('td', row).off('click');
        $('td', row).on('click', () => {
          self.onRowClick(data);
        });

        return row;
      }
    };
  }

  public onRowClick(book: BookModel) {
    this.router.navigate([`/history/${book.id}`]);
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}
