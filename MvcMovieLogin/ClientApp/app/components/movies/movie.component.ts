import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-movies',
    templateUrl: './movie.component.html',
    //styleUrls: ['./movie.component.css']
})
export class MovieComponent {

    public movie: Movie;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute) {
        let id = route.snapshot.params['id'];
        http.get(baseUrl + 'api/movies/' + id).subscribe(result => {
            this.movie = result.json() as Movie;
        }, error => console.error(error));
    }
}

interface Movie {
    movieID: number;
    title: string;
    releaseDate: number;
    genre: string;
    price: number;
    rating: string;
}
