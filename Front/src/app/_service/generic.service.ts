
import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PageResponse } from '../_model/response';
import { PageResponseOne } from '../_model/responseOne';

@Injectable({
  providedIn: 'root'
})
export class GenericService<T> {

  constructor(
    protected _http: HttpClient,
    @Inject(String) protected _url: string
  ) { }

  getItems() {
    return this._http.get<PageResponse<T>>(`${this._url}/All`);
  }

  getItemById(id: string) {
    return this._http.get<PageResponseOne<T>>(`${this._url}/${id}`);
  }

  saveItem(t: T) {
    return this._http.post(`${this._url}/`, t);
  }

  updateItem(t: T , id : any) {
    return this._http.put(`${this._url}/${id}`, t);
  }

  deleteItem(id: number) {
    return this._http.delete(`${this._url}/${id}`);
  }

}
