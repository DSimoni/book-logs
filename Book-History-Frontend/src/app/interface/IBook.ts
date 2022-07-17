export interface IBook {
    id: number;
    title: string;
    description: string;
    publishDate: Date;
    authors:[
      {
        "authorId": number
        "authorName": string,
        "books": []
      }
    ]
  }
  
  