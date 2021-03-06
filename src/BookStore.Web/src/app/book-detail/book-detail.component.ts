import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { AuthorModel } from '../models/author.model';
import { BookHistoryModel } from '../models/book-history.model';
import { BookModel } from '../models/book.model';
import { BookService } from '../services/book.service';

@Component({
  selector: 'app-book-detail',
  templateUrl: './book-detail.component.html'
})
export class BookDetailComponent implements OnInit {

  dtTrigger: Subject<any> = new Subject<any>();
  dtOptions: any = {};
  book: BookModel = new BookModel();
  bookHistory: Array<BookHistoryModel>;
  isEdit: boolean = true;
  form: FormGroup;
  submitted = false;

  constructor(private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef, private formBuilder: FormBuilder, private bookService: BookService) { }

  ngOnInit(): void {
    this.initForm();
    this.getBook();
  }

  ngAfterContentChecked(): void {
    this.ref.detectChanges();
  }

  removeAuthor(author: AuthorModel): void {
    this.book.authorsList = this.book.authorsList.filter(item => item.name != author.name);
  }

  addAuthor(): void {
    const newAuthor = new AuthorModel("");
    this.book.authorsList.push(newAuthor);
  }

  addNew(): void {
    this.reloadCurrentRoute("null");
  }

  backToList(): void {
    this.router.navigate(["books"]);
  }

  onSubmit(): void {

    this.submitted = true;

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.book.authors = this.book.authorsList.map(a => a.name).join(",");

    this.bookService.saveBook(this.book, this.isEdit).subscribe(result => {
      if (result && result.isValid && result.data) {
        alert("Book information saved");
        this.reloadCurrentRoute(result.data.id);
      }
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  private reset() {
    this.submitted = false;
    this.form.markAsPristine();
    this.form.markAsUntouched();
    this.form.updateValueAndValidity();
  }

  private getBook(): void {

    const bookId = this.route.snapshot.paramMap.get('id');
    this.isEdit = bookId != null && bookId != "0" && bookId != "null";

    if (this.isEdit) {
      this.bookService.getBook(bookId).subscribe(book => {
        this.book = book;
        this.book.authorsList = book.authors.split(",").map(a => {
          let author = new AuthorModel(a);
          return author;
        });
      });

      this.getBookHistory(bookId);
    }
  }

  private getBookHistory(bookId: string): void {
    this.dtOptions = {
      pagingType: "simple_numbers",
      lengthMenu: [5, 10, 25, 50, 100],
      pageLength: 10,
      serverSide: true,
      processing: true,
      ajax: (dataTableParameters: any, callback) => {
        this.bookService.listBookHistoryDataTable(dataTableParameters, { data: bookId }).subscribe(page => {
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
        { data: "changeText", name: "Change" },
        { data: "timestamp", name: "Timestamp", searchable: false }
      ],
      //dom: '<fB<t>ip>'
    };
  }

  private initForm(): void {
    this.form = this.formBuilder.group({
      title: [null, Validators.required],
      description: [null, Validators.required],
      publishDate: [null, Validators.required],
      authors: [null, null],
    });
  }

  private reloadCurrentRoute(bookId: string) {
    const currentUrl = `books/detail/${bookId}`;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}
