import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'app-movies',
    templateUrl: './movies.component.html'
})
export class MoviesComponent {
    public movies: MovieData[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/Movies').subscribe(result => {
            this.movies = result.json() as MovieData[];
        }, error => console.error(error));
    }

    getMovie(id: number) {
        let movie = this.movies.find(b => b.movieID == id);
        if (movie) {
            console.log("Selected: " + movie.title);
        }
    }
}

interface MovieData {
    movieID: number;
    title: string;
    releaseDate: number;
    genre: string;
    price: number;
    rating: string;
}
