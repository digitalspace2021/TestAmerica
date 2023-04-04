import { startWith, map, debounceTime } from 'rxjs/operators';
import { FormUsuarioComponent } from './form-usuario/form-usuario.component';
import { Observable } from 'rxjs';
import { Usuario } from '../../_model/usuario.interface';
import { CiudadService } from '../../_service/ciudad.service';
import { Component, OnInit } from '@angular/core';
import { Validators, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { NgbModal, NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';
import { Ciudad } from 'src/app/_model/ciudad.interface';



@Component({
  selector: 'app-usuario',
  templateUrl: './usuario.component.html',
  styleUrls: ['./usuario.component.css']
})
export class UsuarioComponent implements OnInit {

  ciudades!: Ciudad[];
  usuario!: Ciudad;
  form!: FormGroup;
  filter: FormControl = new FormControl("");

  constructor(
    private _ciudadeservice: CiudadService,
    private _formBuilder: FormBuilder,
    private _modalService: NgbModal,
    private spinner : NgxSpinnerService,
    private toastr: ToastrService

  ) { }

  ngOnInit(): void {
    this.getciudades()
  }
  
  openModal( id?: any) {
    let modal = this._modalService.open(FormUsuarioComponent)
    modal.componentInstance.idUsuario = id

    modal.result.then(
      () => {
        this.getciudades()
      }
    )
  }

  eliminar(id: any) {
      Swal.fire({
        title: '¿Está seguro de realizar esta acción?',
        text: "Esta acción no se puede deshacer",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#F8E12E',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Eliminelo!',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
          this._ciudadeservice.deleteItem((id)).subscribe(data => {
              this.toastrMenssage('Eliminado Exitosamente', 'success')
              this.getciudades()
          })
        }
    })
  }

  getciudades() {
    // this.spinner.show();
    this._ciudadeservice.getItems().subscribe(({data}) => {
      this.ciudades = data
      console.log(this.ciudades);
      this.toastrMenssage('ciudades obtenidos Exitosamente', 'success')
      // this.spinner.hide();
    });
  }

  toastrMenssage(Mensage : string , estado : "error" | "success"){
    if(estado ==  "success") {
      this.toastr.success( `${Mensage}` , 'Mensaje del sistema!',{
        positionClass: 'toast-bottom-right',
      });
    }else{
      this.toastr.error( `Ocurrio un error` , 'Mensaje del sistema!',{
        positionClass: 'toast-bottom-right',
      });
    }
  }


}
