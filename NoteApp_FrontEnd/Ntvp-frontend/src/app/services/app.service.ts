import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Injectable, OnInit} from '@angular/core';
import {NoteViewModel} from '../models/noteModel';
import {NotesFilter} from '../models/notesFilter';

@Injectable({
  providedIn: 'root',
})
export class AppService {

  private readonly NotesAppUrl: string;
  private Header: HttpHeaders;
  private httpOptions: { headers: HttpHeaders };

  constructor(private http: HttpClient) {
      this.httpOptions = {headers:   null};
      this.NotesAppUrl = 'http://localhost/NoteApp/api/Notes';
      this.Header = new HttpHeaders({
              'Content-Type':  'application/json',
              'Access-Control-Allow-Origin': '*',
              'Access-Control-Request-Method': 'POST'
          });
      this.httpOptions.headers = this.Header;
      this.http.options('', this.httpOptions);
  }

  public Get(filter: NotesFilter | null): any {
    let url = this.NotesAppUrl + '?';

    if (filter?.TextNameFilter !== undefined) {
        url = url + 'textNameFilter=' + filter.TextNameFilter + '&';
    }
    if (filter?.CategoryFilter !== undefined) {
        url = url + 'CategoryFilter=' + filter.CategoryFilter;
    }
    return this.http.get<NoteViewModel[]>(url);
  }

  public Put(noteModel: NoteViewModel): any {
      return this.http.put<NoteViewModel>( this.NotesAppUrl, noteModel);
  }

  public Post(noteModel: NoteViewModel): any {
      return this.http.post<any>(this.NotesAppUrl, noteModel);
  }

  public Delete(id: any): any {
      return this.http.delete<NoteViewModel>(this.NotesAppUrl + '/' + id);
  }

  public DeleteAll(): any {
      return this.http.delete<NoteViewModel>(this.NotesAppUrl + '/RemoveAll');
  }
}
