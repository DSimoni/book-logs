import { Component, OnInit } from '@angular/core';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { IBook } from 'src/app/interface/IBook';
import { AuthorService } from 'src/app/services/author.service';
import { BookService } from 'src/app/services/book.service';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css'],
})
export class AddBookComponent implements OnInit {
  book = {} as IBook;
  submitted = false;

  dropdownList: any = [];
  selectedItems: any = [];
  dropdownSettings: IDropdownSettings = {};

  constructor(
    private bookService: BookService,
    private authorService: AuthorService
  ) { }
  ngOnInit(): void {
    this.retrieveauthors();

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'item_id',
      textField: 'item_text',
      selectAllText: 'Select All',
      unSelectAllText: 'UnSelect All',
      itemsShowLimit: 3,
      allowSearchFilter: true,
    };
  }

  retrieveauthors(): void {
    let tmp: { item_id: string; item_text: string }[] = [];

    this.authorService.getAll().subscribe(
      (authors) => {
        authors.forEach(
          (author: { authorId: string; authorName: string }) => {
            tmp.push({ item_id: author.authorId, item_text: author.authorName });
          }
        );

        this.dropdownList = tmp;

        console.log(this.dropdownList);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  savebook(): void {

    let temp: any[] = [];

    this.selectedItems.forEach(
      (selectedItem: any) => {
        temp.push({ authorId: selectedItem.item_id, authorName: selectedItem.item_text, books: [] });
      });

    let data = {
      id: 0,
      title: this.book.title,
      description: this.book.description,
      publishDate: new Date(),
      authors: temp,
    };



    this.bookService.create(data).subscribe(
      (response) => {
        console.log(response);
        this.submitted = true;
      },
      (error) => {
        console.log(error);
      }
    );
  }
  newbook(): void {
    this.submitted = false;
    this.book = {
      id: 0,
      title: '',
      description: '',
      publishDate: new Date(),
      authors: [
        {
          authorId: 0,
          authorName: ''
        },
      ],
    };
  }

  onItemSelect(item: any) {
    console.log(item);
  }
  onSelectAll(items: any) {
    console.log(items);
  }
}
