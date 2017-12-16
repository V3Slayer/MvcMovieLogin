import { Component, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Http, Headers } from '@angular/http';
//import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from '@angular/router';

@Component({
    selector: 'app-add-movie',
    templateUrl: './add-movie.component.html',
    //styleUrls: ['./add-book.component.css']
})
export class AddMovieComponent {
    model = new MovieVM();
    postResult: Object;


    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
        http.get(baseUrl + 'api/Movies').subscribe(result => {
            this.model = result.json() as MovieVM;
        }, error => console.error(error));
    }

    onSubmit(form: NgForm) {
        console.log("Submitted: " + this.model.title + " ID: " + this.model.movieID +
            " Year: " + this.model.releaseDate + " Genre: " + this.model.genre);
        let hdrs = new Headers({ 'Content-Type': 'application/json' });

        this.http.post(this.baseUrl + 'api/Movies', this.model,
            { headers: hdrs }).subscribe(result => {
                this.postResult = result;
                this.router.navigate(['/movies']);
            });
    }



}

class MovieVM {
    movieID: number;
    title: string;
    releaseDate: number;
    genre: string;
    price: number;
    rating: string;
}