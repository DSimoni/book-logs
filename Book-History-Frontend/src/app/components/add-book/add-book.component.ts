import { Component, OnInit } from '@angular/core';
import { IBook } from 'src/app/interface/IBook';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {

  book = {} as IBook;
  submitted = false;
  constructor(private bookService: BookService) { }
  ngOnInit(): void {
  }
  savebook(): void {
    const data = {
      id: 0,
      title: this.book.title,
      description: this.book.description,
      publishDate: new Date(),
      authors:[
        {
          "authorId": 0,
          "authorName": "",
          "books": []
        }
      ]
    };
    this.bookService.create(data)
      .subscribe(
        response => {
          console.log(response);
          this.submitted = true;
        },
        error => {
          console.log(error);
        });
  }
  newbook(): void {
    this.submitted = false;
    this.book = {
      id: 0,
      title: '',
      description: '',
      publishDate: new Date(),
      authors: [{"authorId": 0,
      "authorName": "",
      "books": []}]
    };
  }

}
