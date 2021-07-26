import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { BookHistoryModel } from '../models/book-history.model';
import { BookService } from '../services/book.service';

@Component({
  selector: 'app-book-history',
  templateUrl: './book-history.component.html',
  styleUrls: ['./book-history.component.scss']
})
export class BookHistoryComponent implements OnInit {

  dtTrigger: Subject<any> = new Subject<any>();
  public dtOptions: any = {};
  
  constructor(private route: ActivatedRoute, private bookService: BookService) { }

  public bookHistory: Array<BookHistoryModel>;

  ngOnInit(): void {

    const bookId = this.route.snapshot.paramMap.get('id');

    this.dtOptions = {
      pagingType: "simple_numbers",
      pageLength: 20,
      serverSide: true,
      processing: true,
      ajax: (dataTableParameters: any, callback) => {
        this.bookService.listBookHistoryDataTable(dataTableParameters, { data : bookId }).subscribe(page => {
          this.bookHistory = page.data;
          callback({
            recordsTotal: page.recordsTotal,
            recordsFiltered: page.recordsFiltered,
            data: page.data
          });
        })
      },
      columns: [
        { data: "action", name: "Action" },
        { data: "description", name: "Description" },
        { data: "timestamp", name: "Timestamp", searchable: false },
        { data: "user", name: "User", sortable: true, searchable: false },
      ],
      dom: '<fB<t>ip>'
    };
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}
