import { Usuario } from 'src/app/_model/usuario.interface';
import { CiudadService } from '../../../_service/ciudad.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Ciudad } from 'src/app/_model/ciudad.interface';
import { PageResponseOne } from 'src/app/_model/responseOne';

@Component({
  selector: 'app-form-usuario',
  templateUrl: './form-usuario.component.html',
  styleUrls: ['./form-usuario.component.css']
})
export class FormUsuarioComponent implements OnInit {

  idUsuario : any
  
  localStorage = localStorage
  form!: FormGroup;
  departamentos!: any[];
  usuario!: Ciudad;

  constructor(
    private _usuarioService: CiudadService,
    private _formBuilder: FormBuilder,
    public _activeModal: NgbActiveModal,
    private toastr: ToastrService

  ) { }

  ngOnInit(): void {
    this.initForm();
    this.getDepartamentos();
  }

  getDepartamentos(){
    // this._ciudadeservice.getItems().subscribe(data => {
    //   this.departamentos = data
    // });
  }

  emptyForm() {
    this.form = this._formBuilder.group({
      nombre : [null, []],
      codedep : [null, [Validators.required ]]
    });
  }

  editForm() {
    this._usuarioService.getItemById(this.idUsuario.toString()).subscribe((data : any) => {
      this.usuario = data.data
      console.log(this.usuario);
      this.form = this._formBuilder.group({
        id : [this.usuario.codCiu],
        nombre : [this.usuario.nombre, []],
        codedep : [this.usuario.codDep, [Validators.required ]]
      });
    })
  }

  initForm() {
    console.log(this.idUsuario);
    this.emptyForm();
    if (this.idUsuario) {
      this.editForm();
    }
  }

  operate() {
    let rawValuesForm = this.form.getRawValue();
    console.log(rawValuesForm);
    let usuario = {
      ...rawValuesForm
    }

    if (this.usuario) {
      this._usuarioService.updateItem(usuario ,usuario.codCiu).subscribe((data : any) => {
      this.toastrMenssage('Actualizado Exitosamente', 'success')
        this.closeModal();
      });
    } else {
      this._usuarioService.saveItem(usuario).subscribe((data : any) => {
        this.toastrMenssage('Guardado Exitosamente', 'success')
        this.closeModal();
      });
    }
  }

  closeModal() {
    this._activeModal.close();
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
