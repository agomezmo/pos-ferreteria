import { useState, useRef } from 'react';

interface CsvImportModalProps {
  show: boolean;
  onClose: () => void;
  title: string;
  sampleCsv: string;
  onImport: (rows: Record<string, string>[]) => Promise<{ success: number; errors: { row: number; message: string }[] }>;
}

function parseCsv(text: string): Record<string, string>[] {
  const lines: string[] = [];
  let current = '';
  let inQuotes = false;
  for (let i = 0; i < text.length; i++) {
    const ch = text[i];
    if (inQuotes) {
      if (ch === '"') {
        if (i + 1 < text.length && text[i + 1] === '"') {
          current += '"';
          i++;
        } else {
          inQuotes = false;
        }
      } else {
        current += ch;
      }
    } else {
      if (ch === '"') {
        inQuotes = true;
      } else if (ch === '\n') {
        lines.push(current);
        current = '';
      } else if (ch === '\r') {
        // skip
      } else {
        current += ch;
      }
    }
  }
  if (current) lines.push(current);

  if (lines.length < 2) return [];
  const headers = lines[0].split(',').map(h => h.trim());
  const result: Record<string, string>[] = [];
  for (let r = 1; r < lines.length; r++) {
    if (!lines[r].trim()) continue;
    const vals = lines[r].split(',').map(v => v.trim());
    const row: Record<string, string> = {};
    headers.forEach((h, i) => { row[h] = vals[i] ?? ''; });
    result.push(row);
  }
  return result;
}

export default function CsvImportModal({ show, onClose, title, sampleCsv, onImport }: CsvImportModalProps) {
  const fileRef = useRef<HTMLInputElement>(null);
  const [preview, setPreview] = useState<Record<string, string>[] | null>(null);
  const [fileName, setFileName] = useState('');
  const [importing, setImporting] = useState(false);
  const [result, setResult] = useState<{ success: number; errors: { row: number; message: string }[] } | null>(null);

  if (!show) return null;

  const handleFile = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;
    setFileName(file.name);
    setResult(null);
    const reader = new FileReader();
    reader.onload = () => {
      const rows = parseCsv(reader.result as string);
      setPreview(rows);
    };
    reader.readAsText(file);
  };

  const downloadSample = () => {
    const blob = new Blob([sampleCsv], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = `${title.toLowerCase().replace(/\s+/g, '_')}_sample.csv`;
    a.click();
    URL.revokeObjectURL(url);
  };

  const handleImport = async () => {
    if (!preview || preview.length === 0) return;
    setImporting(true);
    setResult(null);
    try {
      const res = await onImport(preview);
      setResult(res);
    } catch (err: any) {
      setResult({ success: 0, errors: [{ row: 0, message: err.message || 'Error inesperado' }] });
    } finally {
      setImporting(false);
    }
  };

  const reset = () => {
    setPreview(null);
    setFileName('');
    setResult(null);
    if (fileRef.current) fileRef.current.value = '';
  };

  const handleClose = () => {
    reset();
    onClose();
  };

  const headerKeys = preview && preview.length > 0 ? Object.keys(preview[0]) : [];

  return (
    <div className="modal-overlay" onClick={handleClose}>
      <div className="modal modal-lg" onClick={e => e.stopPropagation()}>
        <h2>Importar {title}</h2>

        {!result && (
          <>
            <div className="import-help">
              <p>Selecciona un archivo CSV con los datos a importar. La primera fila debe contener los nombres de las columnas.</p>
              <button className="btn-link" onClick={downloadSample}>⬇ Descargar archivo CSV de ejemplo</button>
            </div>

            <div className="import-upload">
              <input ref={fileRef} type="file" accept=".csv" onChange={handleFile} />
              {fileName && <span className="import-filename">{fileName}</span>}
            </div>

            {preview && (
              <div className="import-preview">
                <p><strong>{preview.length} registro(s)</strong> detectado(s) — mostrando primeros 5:</p>
                <div className="table-container" style={{ maxHeight: '250px' }}>
                  <table className="table table-sm">
                    <thead>
                      <tr>{headerKeys.map(k => <th key={k}>{k}</th>)}</tr>
                    </thead>
                    <tbody>
                      {preview.slice(0, 5).map((row, i) => (
                        <tr key={i}>
                          {headerKeys.map(k => <td key={k}>{row[k]}</td>)}
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            )}

            <div className="modal-actions">
              <button className="btn-secondary" onClick={handleClose}>Cancelar</button>
              <button className="btn-primary" onClick={handleImport} disabled={!preview || preview.length === 0 || importing}>
                {importing ? 'Importando...' : 'Importar'}
              </button>
            </div>
          </>
        )}

        {result && (
          <div className="import-result">
            {result.errors.length === 0 ? (
              <div className="import-success">
                <p>✅ <strong>{result.success}</strong> registro(s) importados correctamente.</p>
              </div>
            ) : (
              <div>
                <p>⚠️ <strong>{result.success}</strong> exitoso(s), <strong>{result.errors.length}</strong> error(es).</p>
                <div className="table-container" style={{ maxHeight: '200px' }}>
                  <table className="table table-sm">
                    <thead><tr><th>Fila</th><th>Error</th></tr></thead>
                    <tbody>
                      {result.errors.map((e, i) => (
                        <tr key={i}><td>{e.row}</td><td>{e.message}</td></tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>
            )}
            <div className="modal-actions">
              <button className="btn-primary" onClick={handleClose}>Cerrar</button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
