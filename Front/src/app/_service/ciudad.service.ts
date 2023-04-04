import { HttpClient } from '@angular/common/http';
import { GenericService } from './generic.service';
import { Injectable } from '@angular/core';
import { Ciudad } from '../_model/ciudad.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CiudadService extends GenericService<Ciudad> {

  constructor(
    protected override _http: HttpClient
  ) {
    super(
      _http,
      `${environment.HOST}ciudad`
    )
   }

   


}
