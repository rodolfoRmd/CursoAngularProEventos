import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  evento = {} as Evento;
  form: FormGroup;
  isAddMode: boolean;

  get f(): any {
    return this.form.controls;
  }
  get bsConfig(): any {
    return {
      // adaptivePosition: true ,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }
  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private router: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) {
    this.localeService.use('pt-br');
  }

  public carregarEvento(): void {
    const eventoIdParam = this.router.snapshot.paramMap.get('id');
    this.isAddMode = !eventoIdParam;
    console.log(this.isAddMode);

    if (eventoIdParam != null) {
      this.spinner.show();
      this.eventoService.getEventoById(+eventoIdParam).subscribe(
        (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
        },
        (error: any) => {
          console.log(error);
          this.toastr.error('Erro ao tentar carregar o evento');
          this.spinner.hide();
        },
        () => {
          this.spinner.hide();
        }
      );
    }
  }

  ngOnInit(): void {
    this.validation();
    this.carregarEvento();
  }

  public validation(): void {
    this.form = this.fb.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdePessoas: ['', [Validators.required, Validators.max(120000)]],

      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: ['', Validators.required],
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }
  public salvarAlteracao(): void {
    this.spinner.show();
    if (this.form.valid) {
      if (this.isAddMode) {
        this.evento = { ...this.form.value };

        this.eventoService.postEvento(this.evento).subscribe(
          () => {
            this.toastr.success('Sucesso ao salvar novo evento', 'Sucesso');
          },
          (error: any) => {
            console.error(error);

            this.toastr.error('Erro ao salvar evento', 'Evento');
          }


        ).add(()=>this.spinner.hide());
      }else{
        this.evento = {id:this.evento.id, ...this.form.value };
        this.eventoService.putEvento(this.evento).subscribe(
          () => {
            this.toastr.success('Sucesso ao atualizar evento', 'Sucesso');
          },
          (error: any) => {
            console.error(error);

            this.toastr.error('Erro ao salvar evento', 'Evento');
          }
          ).add(()=>this.spinner.hide());
      }
    }

  }
}
